using TaskManagement.Common.Enums;

namespace TaskManagement.Domain.Dtos.Notification;

public record NotificationGet(Guid Id, string Message, string Type);
