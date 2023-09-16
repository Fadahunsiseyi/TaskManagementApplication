using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Dtos.Task;
using TaskManagement.Domain.Interface.Services;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    public ITaskService TaskService { get; }

    public TaskController(ITaskService taskService)
    {
        TaskService = taskService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateTask(TaskCreate taskCreate)
    {
        var id = await TaskService.CreateTaskAsync(taskCreate);
        return Ok(id);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetTask(Guid id)
    {
        var task = await TaskService.GetTaskDetailsAsync(id);
        return Ok(task);
    }
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await TaskService.GetTasksAsync();
        return Ok(tasks);
    }
    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateTask([FromRoute]Guid id, TaskUpdate taskUpdate)
    {
        await TaskService.UpdateTaskAsync(id, taskUpdate);
        return Ok();
    }
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteTask([FromRoute]Guid id)
    {
        await TaskService.DeleteTaskAsync(id);
        return Ok();
    }
}
