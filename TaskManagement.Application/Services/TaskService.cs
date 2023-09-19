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
        IGenericRepository<Project> projectRepository, TaskCreateValidator taskCreateValidator, TaskUpdateValidator taskUpdateValidator, IGenericRepository<Notification> notificationRepository)
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

        if (!await UserRepository.ExistsAsync(taskCreate.UserId) || !await ProjectRepository.ExistsAsync(taskCreate.ProjectId)) throw new Exception("user or project not found");
       

        string taskStatus;
        switch (taskCreate.Status.ToUpper().Replace(" ", ""))
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
        switch (taskCreate.Priority.ToUpper().Replace(" ", ""))
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

        var entity = Mapper.Map<Domain.Entities.Task>(taskCreate);

        entity.Status = taskStatus;
        entity.Priority = taskPriority;
        entity.DueDate = DateTime.UtcNow.AddDays(7);

        await TaskRepository.InsertAsync(entity);
        await TaskRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<TaskDetails> GetTaskDetailsAsync(Guid id)
    {
        var entity = await TaskRepository.GetByIdAsync(id, (task) => task.User, (task) => task.Project);
        return Mapper.Map<TaskDetails>(entity);
    }
    public async Task<IEnumerable<TaskList>> GetTasksAsync(TaskFilter taskFilter)
    {
        string? lowercasePriority = taskFilter.Priority?.ToUpper().Replace(" ", "");
        string? lowercaseStatus = taskFilter.Status?.ToUpper().Replace(" ", "");

        Expression<Func<Domain.Entities.Task, bool>> statusFilter = (task) => lowercaseStatus == null ? true :
task.Status.StartsWith(lowercaseStatus);
        Expression<Func<Domain.Entities.Task, bool>> priorityFilter = (priority) => lowercasePriority == null ? true :
priority.Priority.StartsWith(lowercasePriority);

        var entities = await TaskRepository.GetFilteredAsync(new Expression<Func<Domain.Entities.Task, bool>>[] { statusFilter, priorityFilter }, (task) => task.User, (task) => task.Project);

        
        return Mapper.Map<IEnumerable<TaskList>>(entities);
    }
    public async System.Threading.Tasks.Task UpdateTaskAsync(Guid id, TaskUpdate taskUpdate)
    {
        await TaskUpdateValidator.ValidateAndThrowAsync(taskUpdate);

        if (!await TaskRepository.ExistsAsync(id)) throw new Exception("Task not found");

        var existingEntity = await TaskRepository.GetByIdAsync(id);
        var entity = Mapper.Map(taskUpdate, existingEntity);
        TaskRepository.Update(entity);
        await TaskRepository.SaveChangesAsync();
    }
    public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id)
    {
        if (!await TaskRepository.ExistsAsync(id)) throw new Exception("Task not found");
        var entity = await TaskRepository.GetByIdAsync(id);
        TaskRepository.Delete(entity);
        await TaskRepository.SaveChangesAsync();
    }
    public async System.Threading.Tasks.Task TaskAssignmentAsync(TaskAssignment taskAssignment)
    {
        try
        {
            if (!await TaskRepository.ExistsAsync(taskAssignment.TaskId) || !await ProjectRepository.ExistsAsync(taskAssignment.ProjectId))
            {
             throw new Exception("Task or Project not found");
            }
            else
            {
                var task = await TaskRepository.GetByIdAsync(taskAssignment.TaskId);

                task.ProjectId = taskAssignment.ProjectId;
                await TaskRepository.SaveChangesAsync();
                var notification = new Notification
                {
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

        if (!await TaskRepository.ExistsAsync(id))
        {
            throw new Exception("Task not found");
        }
        var task = await TaskRepository.GetByIdAsync(id);
        task.ProjectId = null;
        await TaskRepository.SaveChangesAsync();
        var notification = new Notification
        {
            UserId = task.UserId.Value,
            Message = "You have been unassigned from a task",
            Type = NotificationsType.StatusUpdate,
            IsRead = false
        };
        await NotificationRepository.InsertAsync(notification);
        await NotificationRepository.SaveChangesAsync();
    }
    public async Task<IEnumerable<TaskList>> GetTaskDueDateAsync()
    {
        var entities = await TaskRepository.GetFilteredAsync(new Expression<Func<Domain.Entities.Task, bool>>[] { (task) => task.DueDate <= DateTime.UtcNow.AddDays(7) }, (task) => task.User, (task) => task.Project);
        return Mapper.Map<IEnumerable<TaskList>>(entities);
    }

}