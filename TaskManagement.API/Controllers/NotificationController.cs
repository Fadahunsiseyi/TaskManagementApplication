using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Dtos.Notification;
using TaskManagement.Domain.Interface.Services;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private INotificationService NotificationService { get; }
    public NotificationController( INotificationService notificationService )
    {
        NotificationService = notificationService;
    }
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateNotification(NotificationCreate notificationCreate)
    {
        var id = await NotificationService.CreateNotificationAsync(notificationCreate);
        return Ok(id);
    }
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetNotifications()
    {
        var notifications = await NotificationService.GetNotificationsAsync();
        return Ok(notifications);
    }
    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetNotification(Guid id)
    {
        var notification = await NotificationService.GetNotificationAsync(id);
        return Ok(notification);
    }
}
