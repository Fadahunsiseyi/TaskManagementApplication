using TaskManagement.Common.Enums;

namespace TaskManagement.Common.Dtos.Notification;

public record NotificationGet(Guid Id, string Message, string Type, bool IsRead);
