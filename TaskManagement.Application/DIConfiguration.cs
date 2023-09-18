using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Services;
using TaskManagement.Application.Validation;
using TaskManagement.Application.Interface.Services;

namespace TaskManagement.Application;

public class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<INotificationService, NotificationService>();

        services.AddScoped<UserCreateValidator>();
        services.AddScoped<UserUpdateValidator>();
        services.AddScoped<ProjectCreateValidator>();
        services.AddScoped<ProjectUpdateValidator>();
        services.AddScoped<TaskCreateValidator>();
        services.AddScoped<TaskUpdateValidator>();
    }
}
