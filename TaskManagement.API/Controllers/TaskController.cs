using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Dtos.Task;
using TaskManagement.Domain.Entities;
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
        try
        {
            var id = await TaskService.CreateTaskAsync(taskCreate);
            return Ok(new { Status = "Success", Message = "Task created successfully", Id = id });
        }
        catch (Exception ex)
        {

            return BadRequest(new { Status = "Error", Message = "Error creating task: " + ex.Message });
        }
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetTask(Guid id)
    {
        try
        {
            var task = await TaskService.GetTaskDetailsAsync(id);
            if (task != null)
                return Ok(new { Status = "Success", Message = "Task retrieved successfully", Task = task });
            else
                return NotFound(new { Status = "Error", Message = "Task not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Error", Message = "An error occurred while retrieving the task: " + ex.Message });
        }
    }
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetTasks([FromQuery]TaskFilter taskFilter)
    {
        try
        {
            var tasks = await TaskService.GetTasksAsync(taskFilter);
            if (tasks.Any())
                return Ok(new { Status = "Success", Message = "Tasks retrieved successfully", Tasks = tasks });
            else
                return NotFound(new { Status = "Error", Message = "No tasks found" });
        }
        catch (Exception)
        {

            throw;
        }
    }
    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateTask([FromRoute]Guid id, TaskUpdate taskUpdate)
    {
        try
        {
            await TaskService.UpdateTaskAsync(id, taskUpdate);
            return Ok(new { Status = "Success", Message = "Task updated successfully" });
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { Status = "Error", Message = "An error occurred while updating the task: " + ex.Message });
        }
    }
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteTask([FromRoute]Guid id)
    {
        try
        {
            await TaskService.DeleteTaskAsync(id);
            return Ok(new { Status = "Success", Message = "Task deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Error", Message = "An error occurred while deleting the tasjs: " + ex.Message });
        }
    }
}
