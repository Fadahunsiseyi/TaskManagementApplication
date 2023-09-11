using TaskManagement.API.Common;

namespace TaskManagement.API.Models;

public class Notifications : BaseEntity
{
    public NotificationsType Type { get; set; }
    public required string Message { get; set; }
    public DateTime Timestamp { get; set; }
}
