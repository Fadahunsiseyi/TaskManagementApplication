using TaskManagement.Common.Enums;

namespace TaskManagement.Domain.Dtos.Notification;

public record NotificationUpdate(string Message, NotificationsType Type);
