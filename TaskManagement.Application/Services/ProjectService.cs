using AutoMapper;
using TaskManagement.Domain.Dtos.Project;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interface.Persistence;
using TaskManagement.Domain.Interface.Services;

namespace TaskManagement.Application.Services;

public class ProjectService : IProjectService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Project> ProjectRepository { get; }

    public ProjectService(IMapper mapper, IGenericRepository<Project> projectRepository)
    {
        Mapper = mapper;
        ProjectRepository = projectRepository;
    }




    public async Task<Guid> CreateProjectAsync(ProjectCreate projectCreate)
    {
        var entity = Mapper.Map<Project>(projectCreate);
        await ProjectRepository.InsertAsync(entity);
        await ProjectRepository.SaveChangesAsync();
        return entity.Id;
    }
}
