using AutoMapper;
using System.Net;
using TaskManagement.Common.Enums;
using TaskManagement.Domain.Dtos.Task;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interface.Persistence;
using TaskManagement.Domain.Interface.Services;

namespace TaskManagement.Application.Services;

public class TaskService : ITaskService
{
    public IMapper Mapper { get; }
    private IGenericRepository<Domain.Entities.Task> TaskRepository { get; }
    private IGenericRepository<User> UserRepository { get; }
    private IGenericRepository<Project> ProjectRepository { get; }

    public TaskService(IMapper mapper, IGenericRepository<Domain.Entities.Task> taskRepository, IGenericRepository<User> userRepository,
        IGenericRepository<Project> projectRepository)
    {
        Mapper = mapper;
        TaskRepository = taskRepository;
        UserRepository = userRepository;
        ProjectRepository = projectRepository;
    }
    public async Task<Guid> CreateTaskAsync(TaskCreate taskCreate)
    {
        var user = await UserRepository.GetByIdAsync(taskCreate.UserId);

        if (user is null) throw new Exception("user not found");

        var project = await ProjectRepository.GetByIdAsync(taskCreate.ProjectId);

        if (project is null) throw new Exception("project not found");

        var entity = Mapper.Map<Domain.Entities.Task>(taskCreate);

        string taskStatus;
        switch (taskCreate.Status.ToLower()) // Use ToLower to handle case-insensitivity
        {
            case "pending":
                taskStatus = "Pending...";
                break;
            case "inprogress":
                taskStatus = "In-Progress...";
                break;
            case "completed":
                taskStatus = "Completed!";
                break;
            default:
                throw new Exception("Invalid task status");
        }
        string taskPriority;
        switch (taskCreate.Priority.ToLower()) // Use ToLower to handle case-insensitivity
        {
            case "low":
                taskPriority = "Low";
                break;
            case "medium":
                taskPriority = "Medium";
                break;
            case "high":
                taskPriority = "High";
                break;
            default:
                throw new Exception("Invalid task priority");
        }


        entity.User = user;
        entity.Project = project;
        entity.Status = taskStatus;
        entity.Priority = taskPriority;
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
    public async Task<IEnumerable<TaskList>> GetTasksAsync()
    {
        var entity = await TaskRepository.GetAllAsync(null,null);
        return Mapper.Map<IEnumerable<TaskList>>(entity);
    }
}