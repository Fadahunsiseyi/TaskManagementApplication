using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Interface.Services;

namespace TaskManagement.Application;

public class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskService, TaskService>();
    }
}
