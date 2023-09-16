using TaskManagement.Domain.Dtos.Task;

namespace TaskManagement.Domain.Interface.Services;

public interface ITaskService
{
    Task<Guid> CreateTaskAsync(TaskCreate taskCreate);
    Task<TaskDetails> GetTaskDetailsAsync(Guid id);
}