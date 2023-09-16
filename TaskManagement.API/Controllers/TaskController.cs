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
}
