using AutoMapper;
using FluentValidation;
using System.Linq.Expressions;
using System.Net;
using TaskManagement.Application.Validation;
using TaskManagement.Common.Enums;
using TaskManagement.Common.Dtos.Task;
using TaskManagement.Common.Dtos.User;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Interface.Persistence;
using TaskManagement.Application.Interface.Services;

namespace TaskManagement.Application.Services;

public class TaskService : ITaskService
{
    public IMapper Mapper { get; }
    private IGenericRepository<Domain.Entities.Task> TaskRepository { get; }
    private IGenericRepository<User> UserRepository { get; }
    private IGenericRepository<Project> ProjectRepository { get; }
    private IGenericRepository<Notification> NotificationRepository { get; }
    private TaskCreateValidator TaskCreateValidator { get; }
    private TaskUpdateValidator TaskUpdateValidator { get; }

    public TaskService(IMapper mapper, IGenericRepository<Domain.Entities.Task> taskRepository, IGenericRepository<User> userRepository,
        IGenericRepository<Project> projectRepository, TaskCreateValidator taskCreateValidator, TaskUpdateValidator taskUpdateValidator, IGenericRepository<Notification> notificationRepository  )
    {
        Mapper = mapper;
        TaskRepository = taskRepository;
        UserRepository = userRepository;
        ProjectRepository = projectRepository;
        TaskCreateValidator = taskCreateValidator;
        TaskUpdateValidator = taskUpdateValidator;
        NotificationRepository = notificationRepository;
    }
    public async Task<Guid> CreateTaskAsync(TaskCreate taskCreate)
    {
        await TaskCreateValidator.ValidateAndThrowAsync(taskCreate);

        var user = await UserRepository.GetByIdAsync(taskCreate.UserId);

        if (user is null) throw new Exception("user not found");

        var project = await ProjectRepository.GetByIdAsync(taskCreate.ProjectId);

        if (project is null) throw new Exception("project not found");

        var entity = Mapper.Map<Domain.Entities.Task>(taskCreate);

        string taskStatus;
        switch (taskCreate.Status.ToUpper().Replace(" ","")) // Use ToLower to handle case-insensitivity
        {
            case "PENDING":
                taskStatus = "PENDING";
                break;
            case "INPROGRESS":
                taskStatus = "INPROGRESS";
                break;
            case "COMPLETED":
                taskStatus = "COMPLETED";
                break;
            default:
                throw new Exception("Invalid task status");
        }
        string taskPriority;
        switch (taskCreate.Priority.ToUpper().Replace(" ", "")) // Use ToLower to handle case-insensitivity
        {
            case "LOW":
                taskPriority = "LOW";
                break;
            case "MEDIUM":
                taskPriority = "MEDIUM";
                break;
            case "HIGH":
                taskPriority = "HIGH";
                break;
            default:
                throw new Exception("Invalid task priority");
        }


        entity.User = user;
        entity.Project = project;
        entity.Status = taskStatus;//to be deleted
        entity.Priority = taskPriority;// to be deleted
        entity.DueDate = DateTime.UtcNow.AddDays(7);
        await TaskRepository.InsertAsync(entity);
        await TaskRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<TaskDetails> GetTaskDetailsAsync(Guid id)
    {
        var entity = await TaskRepository.GetByIdAsync(id, (task) => task.User, (task) => task.Project  );
        return Mapper.Map<TaskDetails>(entity);
    }
    public async Task<IEnumerable<TaskList>> GetTasksAsync(TaskFilter taskFilter)
    {
        string? lowercasePriority = taskFilter.Priority?.ToUpper().Replace(" ", "");
        string? lowercaseStatus = taskFilter.Status?.ToUpper().Replace(" ","");

        Expression<Func<Domain.Entities.Task, bool>> statusFilter = (task) => lowercaseStatus == null ? true :
task.Status.StartsWith(lowercaseStatus);
        Expression<Func<Domain.Entities.Task, bool>> priorityFilter = (priority) => lowercasePriority == null ? true :
priority.Priority.StartsWith(lowercasePriority);

        var entities = await TaskRepository.GetFilteredAsync(new Expression<Func<Domain.Entities.Task, bool>>[] { statusFilter, priorityFilter }, (task) => task.User, (task) => task.Project);

        //var entity = await TaskRepository.GetAllAsync(null,null);
        return Mapper.Map<IEnumerable<TaskList>>(entities);
    }
    public async System.Threading.Tasks.Task UpdateTaskAsync(Guid id, TaskUpdate taskUpdate)
    {
        await TaskUpdateValidator.ValidateAndThrowAsync(taskUpdate);

        var existingEntity = await TaskRepository.GetByIdAsync(id);
        if (existingEntity is null) throw new Exception("Task not found");
        var entity = Mapper.Map(taskUpdate, existingEntity);
        TaskRepository.Update(entity);
        await TaskRepository.SaveChangesAsync();
    }
    public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id)
    {
        var entity = await TaskRepository.GetByIdAsync(id);
        if (entity is null) throw new Exception("Task not found");
        TaskRepository.Delete(entity);
        await TaskRepository.SaveChangesAsync();
    }
    public async System.Threading.Tasks.Task TaskAssignmentAsync(TaskAssignment taskAssignment)
    {
        try
        {
            if(!await TaskRepository.ExistsAsync(taskAssignment.TaskId) || !await ProjectRepository.ExistsAsync(taskAssignment.ProjectId) )
            {
                  throw new Exception("Task or Project not found");
            }
            else
            {
                var task = await TaskRepository.GetByIdAsync(taskAssignment.TaskId);

                task.ProjectId = taskAssignment.ProjectId;
                await TaskRepository.SaveChangesAsync();
                var notification = new Notification { 
                    UserId = task.UserId.Value,
                    Message = "You have been assigned a task",
                    Type = NotificationsType.StatusUpdate,
                    IsRead = false
                };
                await NotificationRepository.InsertAsync(notification);
                await NotificationRepository.SaveChangesAsync();
            }
           
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while assigning the task: " + ex.Message);
        }
    }
    public async System.Threading.Tasks.Task TaskUnAssignmentAsync(Guid id)
    {

            if(!await TaskRepository.ExistsAsync(id))
            {
                  throw new Exception("Task not found");
            }
            var task = await TaskRepository.GetByIdAsync(id);
        task.ProjectId = null;
            await TaskRepository.SaveChangesAsync();
    }

}