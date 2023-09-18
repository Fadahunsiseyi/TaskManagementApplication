using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services;
using TaskManagement.Common.Dtos.Project;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Interface.Services;

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
        try
        {
            var id = await ProjectService.CreateProjectAsync(projectCreate);
            return Ok(new { Status = "Success", Message = "Project created successfully", Id = id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Status = "Error", Message = "Error creating notification: " + ex.Message });
        }
    }
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetProjects()
    {
        try
        {
            var projects = await ProjectService.GetProjectsAsync();
            if (projects.Any())
                return Ok(new { Status = "Success", Message = "Projects retrieved successfully", Projects = projects });
            else
                return NotFound(new { Status = "Error", Message = "No projects found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Error", Message = "An error occurred while retrieving projects: " + ex.Message });
        }
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetProject(Guid id)
    {
        try
        {
            var project = await ProjectService.GetProjectAsync(id);
            if (project != null)
                return Ok(new { Status = "Success", Message = "Project retrieved successfully", Project = project });
            else
                return NotFound(new { Status = "Error", Message = "Project not found" });
        }
        catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = "An error occurred while retrieving the project: " + ex.Message });
            }
    }
    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateProject([FromRoute]Guid id, ProjectUpdate projectUpdate)
    {
        try
        {
            await ProjectService.UpdateProjectAsync(id, projectUpdate);
            return Ok(new { Status = "Success", Message = "Project updated successfully" });
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { Status = "Error", Message = "An error occurred while updating the project: " + ex.Message });
        }
    }
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteProject([FromRoute]Guid id)
    {
        try
        {
            await ProjectService.DeleteProjectAsync(id);
            return Ok(new { Status = "Success", Message = "Project deleted successfully" });
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { Status = "Error", Message = "An error occurred while deleting the project: " + ex.Message });
        }
    }
}
