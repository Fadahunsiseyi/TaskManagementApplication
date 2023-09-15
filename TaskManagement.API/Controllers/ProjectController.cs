using Microsoft.AspNetCore.Mvc;
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
}
