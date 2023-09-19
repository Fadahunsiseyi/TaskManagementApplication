using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskManagement.Common.Enums;
using TaskManagement.Domain.Entities;
using TaskManagement.Persistence;

namespace TaskManagement.Infrastructure.BackgroundService;
public class NotificationService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _factory;
    private readonly ILogger<NotificationService> _logger;
    private Timer _timer;
    private int _executionCount;

    public NotificationService(IServiceScopeFactory factory, ILogger<NotificationService> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    public System.Threading.Tasks.Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(NotificationService)} running...");
        _timer = new Timer(SendNotification, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        return System.Threading.Tasks.Task.CompletedTask;
    }

    public System.Threading.Tasks.Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(NotificationService)} is stopping...");
        _timer?.Change(Timeout.Infinite, 0);
        return System.Threading.Tasks.Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private void SendNotification(object state)
    {
        var count = Interlocked.Increment(ref _executionCount);
        _logger.LogInformation($"{nameof(NotificationService)} is working... | Count: {count}");

        using var scope = _factory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        List<Domain.Entities.Task> tasks = new List<Domain.Entities.Task>();
        List<Notification> notifications = new List<Notification>();

        tasks = context.Tasks.Where(x => x.DueDate.Date == DateTime.Now.Date.AddDays(2)).ToList();
        foreach (var task in tasks)
        {
            notifications.Add(new Notification {
                UserId = task.UserId.Value,
                Message = $"Task {task.Title} is due in 2 days",
               Type =  NotificationsType.DueDateReminder,
               IsRead = false
            });
        };


        var completedTasks = context.Tasks.Where(x => x.Status == "COMPLETED").ToList();
        foreach (var task in completedTasks)
        {
            notifications.Add(new Notification
            {
                UserId = task.UserId.Value,
                Message = $"Task {task.Title} has been marked as completed",
                Type = NotificationsType.StatusUpdate,
                IsRead = false
            });
        }


        var yesterday = DateTime.Now.AddDays(-1);
        var newlyAssignedTasks = context.Tasks
            .Where(task => task.Created >= yesterday)
            .ToList();

        foreach (var task in newlyAssignedTasks)
        {
            notifications.Add(new Notification
            {
                UserId = task.UserId.Value,
                Message = $"You have a new task assigned: {task.Title}",
                Type = NotificationsType.StatusUpdate,
                IsRead = false
            });
        }


        context.Notifications.AddRange(notifications);
        context.SaveChanges();
    }

}
