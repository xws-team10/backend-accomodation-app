using notification_service.Model;

namespace notification_service.Service.Core
{
    public interface INotificationUserSettingsService
    {
        Task<List<NotificationUserSettings>> GetAllAsync();
        Task<NotificationUserSettings> GetByIdAsync(Guid id);
        Task<NotificationUserSettings> GetByUserAsync(Guid id);
        Task CreateAsync(NotificationUserSettings newNotificationUserSettings);
        Task UpdateAsync(Guid id, NotificationUserSettings updateNotificationUserSettings);
        Task DeleteAsync(Guid id);
    }
}
