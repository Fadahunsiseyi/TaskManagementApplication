using TaskManagement.Common.Enums;

namespace TaskManagement.Common.Dtos.Notification;

public record NotificationCreate(string Message, NotificationsType Type, Guid UserId, bool IsRead);