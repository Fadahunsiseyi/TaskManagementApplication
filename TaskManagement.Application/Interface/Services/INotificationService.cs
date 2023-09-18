using TaskManagement.Common.Dtos.Notification;

namespace TaskManagement.Application.Interface.Services;

public interface INotificationService
{
    public Task<Guid> CreateNotificationAsync(NotificationCreate notificationCreate);
    public Task<IEnumerable<NotificationGet>> GetNotificationsAsync();
    public Task<NotificationGet> GetNotificationAsync(Guid id);
    public Task UpdateNotificationAsync(Guid id, NotificationUpdate notificationUpdate);
    public Task DeleteNotificationAsync(Guid id);
}
