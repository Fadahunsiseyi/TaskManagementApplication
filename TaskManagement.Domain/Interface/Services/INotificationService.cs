using TaskManagement.Domain.Dtos.Notification;

namespace TaskManagement.Domain.Interface.Services;

public interface INotificationService
{
    public Task<Guid> CreateNotificationAsync(NotificationCreate notificationCreate);
    public Task<IEnumerable<NotificationGet>> GetNotificationsAsync();
    public Task<NotificationGet> GetNotificationAsync(Guid id);
}
