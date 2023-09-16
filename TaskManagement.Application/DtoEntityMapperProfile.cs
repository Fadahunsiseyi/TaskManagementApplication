using AutoMapper;
using TaskManagement.Domain.Dtos.Project;
using TaskManagement.Domain.Dtos.User;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application;

public class DtoEntityMapperProfile : Profile
{
    public DtoEntityMapperProfile()
    {
        CreateMap<UserCreate, User>()
       .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<UserUpdate, User>();
        CreateMap<UserDelete, User>();
        CreateMap<User, UserGet>();

        CreateMap<ProjectCreate, Project>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<ProjectUpdate, Project>();
        CreateMap<ProjectDelete, Project>();
        CreateMap<Project, ProjectGet>();
    }
}
