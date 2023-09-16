﻿using AutoMapper;
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
        var existingEntity = await ProjectRepository.GetByIdAsync(id);
        if (existingEntity is null) throw new Exception("Project not found");
        var entity = Mapper.Map(projectUpdate, existingEntity);
        ProjectRepository.Update(entity);
        await ProjectRepository.SaveChangesAsync();
    }
    public async System.Threading.Tasks.Task DeleteProjectAsync(Guid id)
    {
        var entity = await ProjectRepository.GetByIdAsync(id);
        if (entity is null) throw new Exception("Project not found");
        ProjectRepository.Delete(entity);
        await ProjectRepository.SaveChangesAsync();
    }
}
