using System.Net;
using TaskManagement.API;
using TaskManagement.Application;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Interface.Persistence;
using TaskManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.BackgroundService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DIConfiguration.RegisterServices(builder.Services);
var dbFilename = Environment.GetEnvironmentVariable("DB_FILENAME");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Filename={dbFilename}"));
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IGenericRepository<Project>, GenericRepository<Project>>();
builder.Services.AddScoped<IGenericRepository<TaskManagement.Domain.Entities.Task>, GenericRepository<TaskManagement.Domain.Entities.Task>>();
builder.Services.AddScoped<IGenericRepository<Notification>, GenericRepository<Notification>>();



builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
