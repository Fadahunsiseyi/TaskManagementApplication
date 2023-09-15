using TaskManagement.Domain.Dtos.Project;

namespace TaskManagement.Domain.Interface.Services;

public interface IProjectService
{
    Task<Guid> CreateProjectAsync(ProjectCreate projectCreate);
}
