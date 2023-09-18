using TaskManagement.Common.Dtos.Project;

namespace TaskManagement.Application.Interface.Services;

public interface IProjectService
{
    Task<Guid> CreateProjectAsync(ProjectCreate projectCreate);
    Task<IEnumerable<ProjectGet>> GetProjectsAsync();
    Task<ProjectGet> GetProjectAsync(Guid id);
    Task UpdateProjectAsync(Guid id, ProjectUpdate projectUpdate);
    Task DeleteProjectAsync(Guid id);
}
