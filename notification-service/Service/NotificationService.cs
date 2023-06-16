using notification_service.Model;
using notification_service.Repository;
using notification_service.Service.Core;

namespace notification_service.Service
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationRepository _repository;

        public NotificationService(NotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Notification>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Notification> GetByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<List<Notification>> GetAllByUserAsync(Guid id) =>
            await _repository.GetAllByUserAsync(id);

        public async Task<List<Notification>> GetUnreadByUserAsync(Guid id) =>
            await _repository.GetUnreadByUserAsync(id);

        public async Task CreateAsync(Notification newNotification) =>
            await _repository.CreateAsync(newNotification);

        public async Task UpdateAsync(Guid id, Notification updateNotification) =>
            await _repository.UpdateAsync(id, updateNotification);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);
    }
}