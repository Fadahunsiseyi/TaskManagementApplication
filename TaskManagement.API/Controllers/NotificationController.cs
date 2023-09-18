using Microsoft.AspNetCore.Mvc;
using TaskManagement.Common.Dtos.Notification;
using TaskManagement.Application.Interface.Services;

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
        try {
            var id = await NotificationService.CreateNotificationAsync(notificationCreate);
            return Ok(new { Status = "Success", Message = "Notification created successfully", Id = id });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Status = "Error", Message = "Error creating notification: " + ex.Message });
        }

    }
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetNotifications()
    {
        try
        {
            var notifications = await NotificationService.GetNotificationsAsync();
            if(notifications.Any())
            return Ok(new { Status = "Success", Message = "Notifications retrieved successfully", Notifications = notifications });
            else
                return NotFound(new { Status = "Error", Message = "No notifications found" });
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { Status = "Error", Message = "An error occurred while retrieving notifications: " + ex.Message });
        }

    }
    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetNotification(Guid id)
    {
        try
        {
            var notification = await NotificationService.GetNotificationAsync(id);

            if (notification != null)
                return Ok(new { Status = "Success", Message = "Notification retrieved successfully", Notification = notification });
            else
                return NotFound(new { Status = "Error", Message = "Notification not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Error", Message = "An error occurred while retrieving the notification: " + ex.Message });
        }
    }
    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateNotification([FromRoute]Guid id, NotificationUpdate notificationUpdate)
    {
        try
        {
            await NotificationService.UpdateNotificationAsync(id, notificationUpdate);

            return Ok(new { Status = "Success", Message = "Notification updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Error", Message = "An error occurred while updating the notification: " + ex.Message });
        }
    }
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteNotification([FromRoute]Guid id)
    {
        try
        {
            await NotificationService.DeleteNotificationAsync(id);

            return Ok(new { Status = "Success", Message = "Notification deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Error", Message = "An error occurred while deleting the notification: " + ex.Message });
        }
    }
}
