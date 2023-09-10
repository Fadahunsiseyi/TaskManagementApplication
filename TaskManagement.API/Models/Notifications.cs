using TaskManagement.API.Common;

namespace TaskManagement.API.Models;

public class Notifications : BaseEntity
{
    public int Id { get; set; }
    public NotificationsType Type { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}
