using TaskManagement.Common.Enums;

namespace TaskManagement.Domain.Dtos.Notification;

public record NotificationCreate(string Message, NotificationsType Type, Guid UserId);