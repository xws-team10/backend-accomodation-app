using notification_service.Model;

namespace notification_service.Repository.Core
{
    public interface INotificationUserSettingsRepository
    {
        Task<List<NotificationUserSettings>> GetAllAsync();
        Task<NotificationUserSettings> GetByIdAsync(Guid id);
        Task<NotificationUserSettings> GetByUserAsync(Guid id);
        Task CreateAsync(NotificationUserSettings newNotificationUserSettings);
        Task UpdateAsync(Guid id, NotificationUserSettings updateNotificationUserSettings);
        Task DeleteAsync(Guid id);
    }
}
