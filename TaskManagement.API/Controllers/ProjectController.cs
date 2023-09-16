using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Dtos.Project;
using TaskManagement.Domain.Interface.Services;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    public IProjectService ProjectService { get; }

    public ProjectController(IProjectService projectService)
    {
        ProjectService = projectService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateProject(ProjectCreate projectCreate)
    {
        var id = await ProjectService.CreateProjectAsync(projectCreate);
        return Ok(id);
    }
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await ProjectService.GetProjectsAsync();
        return Ok(users);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await ProjectService.GetProjectAsync(id);
        return Ok(user);
    }
    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute]Guid id, ProjectUpdate projectUpdate)
    {
        await ProjectService.UpdateProjectAsync(id, projectUpdate);
        return Ok();
    }
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute]Guid id)
    {
        await ProjectService.DeleteProjectAsync(id);
        return Ok();
    }
}
