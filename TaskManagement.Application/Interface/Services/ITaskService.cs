using TaskManagement.Common.Dtos.Task;

namespace TaskManagement.Application.Interface.Services;

public interface ITaskService
{
    Task<Guid> CreateTaskAsync(TaskCreate taskCreate);
    Task<TaskDetails> GetTaskDetailsAsync(Guid id);
    Task<IEnumerable<TaskList>> GetTasksAsync(TaskFilter taskFilter);
    Task UpdateTaskAsync(Guid id, TaskUpdate taskUpdate);
    Task DeleteTaskAsync(Guid id);
    Task TaskAssignmentAsync(TaskAssignment taskAssignment);
    Task TaskUnAssignmentAsync(Guid taskId);
    Task<IEnumerable<TaskList>> GetTaskDueDateAsync();
}