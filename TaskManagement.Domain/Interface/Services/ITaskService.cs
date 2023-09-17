using TaskManagement.Domain.Dtos.Task;

namespace TaskManagement.Domain.Interface.Services;

public interface ITaskService
{
    Task<Guid> CreateTaskAsync(TaskCreate taskCreate);
    Task<TaskDetails> GetTaskDetailsAsync(Guid id);
    Task<IEnumerable<TaskList>> GetTasksAsync(TaskFilter taskFilter);
    Task UpdateTaskAsync(Guid id, TaskUpdate taskUpdate);
    Task DeleteTaskAsync(Guid id);
}