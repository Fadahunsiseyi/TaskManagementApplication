using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Dtos.User;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application
{
    public class DtoEntityMapperProfile : Profile
    {
        public DtoEntityMapperProfile()
        {
            CreateMap<UserCreate, User>()
    .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
