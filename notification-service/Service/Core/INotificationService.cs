using notification_service.Model;

namespace notification_service.Service.Core
{
    public interface INotificationService
    {
        Task<List<Notification>> GetAllAsync();
        Task<Notification> GetByIdAsync(Guid id);
        Task<List<Notification>> GetAllByUserAsync(Guid id);
        Task<List<Notification>> GetUnreadByUserAsync(Guid id);
        Task CreateAsync(Notification newNotification);
        Task UpdateAsync(Guid id, Notification updateNotification);
        Task DeleteAsync(Guid id);
    }
}
