﻿using AutoMapper;
using TaskManagement.Domain.Dtos.Notification;
using TaskManagement.Domain.Dtos.Project;
using TaskManagement.Domain.Dtos.Task;
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

        CreateMap<TaskCreate, Domain.Entities.Task>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<TaskUpdate, Domain.Entities.Task>();
        CreateMap<TaskDelete, Domain.Entities.Task>();
        CreateMap<Domain.Entities.Task, TaskList>();
        //.ForMember(dest => dest.User, opt => opt.Ignore())
        //.ForMember(dest => dest.Project, opt => opt.Ignore());

        CreateMap<Domain.Entities.Task, TaskDetails>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<NotificationCreate, Notification>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Notification, NotificationGet>();

    }
}
