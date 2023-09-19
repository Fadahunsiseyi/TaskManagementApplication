using AutoMapper;
using TaskManagement.Common.Enums;
using TaskManagement.Common.Dtos.Notification;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Interface.Persistence;
using TaskManagement.Application.Interface.Services;

namespace TaskManagement.Application.Services;

public class NotificationService : INotificationService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Notification> NotificationRepository { get; }

    public NotificationService(IMapper mapper, IGenericRepository<Notification> notificationRepository)
    {
        Mapper = mapper;
        NotificationRepository = notificationRepository;
    }

    public async Task<Guid> CreateNotificationAsync(NotificationCreate notificationCreate)
    {
        var entity = Mapper.Map<Notification>(notificationCreate);
        entity.IsRead = false;
        await NotificationRepository.InsertAsync(entity);
        await NotificationRepository.SaveChangesAsync();
        return entity.Id;
    }
    public async Task<IEnumerable<NotificationGet>> GetNotificationsAsync()
    {
        var entities = await NotificationRepository.GetAllAsync(null,null);
        return Mapper.Map<IEnumerable<NotificationGet>>(entities);
    }
    public async Task<NotificationGet> GetNotificationAsync(Guid id)
    {
        if (!await NotificationRepository.ExistsAsync(id)) throw new Exception("Notification not found");
        var entity = await NotificationRepository.GetByIdAsync(id);

        bool handleReadValue = false;
        if(!entity.IsRead) handleReadValue = true;

        var notificationType = entity.Type == NotificationsType.StatusUpdate ? "Status Update" : "DueDate Reminder";
        var notificationGet = new NotificationGet(entity.Id, entity.Message, notificationType, handleReadValue);

        return notificationGet;
    }
    public async System.Threading.Tasks.Task UpdateNotificationAsync(Guid id, NotificationUpdate notificationUpdate)
    {
        if (!await NotificationRepository.ExistsAsync(id)) throw new Exception("Notification not found");

        var existingEntity = await NotificationRepository.GetByIdAsync(id);
        if (existingEntity is null) throw new Exception("Notification not found");
        var entity = Mapper.Map(notificationUpdate, existingEntity);

         NotificationRepository.Update(entity);
        await NotificationRepository.SaveChangesAsync();
    }
    public async System.Threading.Tasks.Task DeleteNotificationAsync(Guid id)
    {
        if (!await NotificationRepository.ExistsAsync(id)) throw new Exception("Notification not found");
        var entity = await NotificationRepository.GetByIdAsync(id);

        NotificationRepository.Delete(entity);
        await NotificationRepository.SaveChangesAsync();
    }
}
