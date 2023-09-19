using AutoMapper;
using FluentValidation;
using TaskManagement.Application.Validation;
using TaskManagement.Common.Dtos.Project;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Interface.Persistence;
using TaskManagement.Application.Interface.Services;

namespace TaskManagement.Application.Services;

public class ProjectService : IProjectService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Project> ProjectRepository { get; }
    private ProjectCreateValidator ProjectCreateValidator { get; }
    private ProjectUpdateValidator ProjectUpdateValidator { get; }

    public ProjectService(IMapper mapper, IGenericRepository<Project> projectRepository, ProjectCreateValidator projectCreateValidator, ProjectUpdateValidator projectUpdateValidator)
    {
        Mapper = mapper;
        ProjectRepository = projectRepository;
        ProjectCreateValidator = projectCreateValidator;
        ProjectUpdateValidator = projectUpdateValidator;
    }

    public async Task<Guid> CreateProjectAsync(ProjectCreate projectCreate)
    {
        await ProjectCreateValidator.ValidateAndThrowAsync(projectCreate);

        var entity = Mapper.Map<Project>(projectCreate);
        await ProjectRepository.InsertAsync(entity);
        await ProjectRepository.SaveChangesAsync();
        return entity.Id;
    }
    public async Task<IEnumerable<ProjectGet>> GetProjectsAsync()
    {
        var entity = await ProjectRepository.GetAllAsync(null,null);
        return Mapper.Map<IEnumerable<ProjectGet>>(entity);
    }
    public async Task<ProjectGet> GetProjectAsync(Guid id)
    {
        var entity = await ProjectRepository.GetByIdAsync(id);
        return Mapper.Map<ProjectGet>(entity);
    }
    public async System.Threading.Tasks.Task UpdateProjectAsync(Guid id, ProjectUpdate projectUpdate)
    {
        if (!await ProjectRepository.ExistsAsync(id)) throw new Exception("Project not found");
        await ProjectUpdateValidator.ValidateAndThrowAsync(projectUpdate);

        var existingEntity = await ProjectRepository.GetByIdAsync(id);
        var entity = Mapper.Map(projectUpdate, existingEntity);
        ProjectRepository.Update(entity);
        await ProjectRepository.SaveChangesAsync();
    }
    public async System.Threading.Tasks.Task DeleteProjectAsync(Guid id)
    {
        if (!await ProjectRepository.ExistsAsync(id)) throw new Exception("Project not found");
        var entity = await ProjectRepository.GetByIdAsync(id);
        ProjectRepository.Delete(entity);
        await ProjectRepository.SaveChangesAsync();
    }
}
