using TaskManagement.Common.Enums;

namespace TaskManagement.Common.Dtos.Notification;

public record NotificationUpdate(string Message, NotificationsType Type);
