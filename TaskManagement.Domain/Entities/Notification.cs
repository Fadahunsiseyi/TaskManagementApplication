using TaskManagement.Common.Enums;

namespace TaskManagement.Domain.Entities;

public class Notification : BaseEntity
{
    public string Message { get; set; }
    public NotificationsType Type { get; set; }
    public Guid UserId { get; set; }
    public bool IsRead { get; set; }

    public virtual User User { get; set; }
}
