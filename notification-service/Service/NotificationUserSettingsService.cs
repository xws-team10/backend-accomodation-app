using notification_service.Model;
using notification_service.Repository;
using notification_service.Service.Core;

namespace notification_service.Service
{
    public class NotificationUserSettingsService : INotificationUserSettingsService
    {
        private readonly NotificationUserSettingsRepository _repository;

        public NotificationUserSettingsService(NotificationUserSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NotificationUserSettings>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<NotificationUserSettings> GetByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<NotificationUserSettings> GetByUserAsync(Guid id)
        {
            NotificationUserSettings userSettings = await _repository.GetByUserAsync(id);
            if (userSettings == null)
            {
                NotificationUserSettings newUserSettings = new NotificationUserSettings();
                newUserSettings.UserId = id;
                await _repository.CreateAsync(newUserSettings);
                return await _repository.GetByUserAsync(id);
            }
            return userSettings;
        }

        public async Task CreateAsync(NotificationUserSettings newNotificationUserSettings) =>
            await _repository.CreateAsync(newNotificationUserSettings);

        public async Task UpdateAsync(Guid id, NotificationUserSettings updateNotificationUserSettings) =>
            await _repository.UpdateAsync(id, updateNotificationUserSettings);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);
    }
}
