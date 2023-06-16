using notification_service.Model;
using notification_service.Repository;
using notification_service.Service.Core;

namespace notification_service.Service
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationRepository _repository;
        private readonly NotificationUserSettingsService _notificationUserSettingsService;

        public NotificationService(NotificationRepository repository, NotificationUserSettingsService notificationUserSettingsService)
        {
            _repository = repository;
            _notificationUserSettingsService = notificationUserSettingsService;
        }

        public async Task<List<Notification>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Notification> GetByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<List<Notification>> GetAllByUserAsync(Guid id)
        {
            NotificationUserSettings userSettings = await _notificationUserSettingsService.GetByUserAsync(id);
            List<NotificationType> types = new List<NotificationType>();
            if (userSettings.showReservationRequestCreated)
                types.Add(NotificationType.RESERVATION_REQUEST_CREATED);
            if (userSettings.showReservationCanceled)
                types.Add(NotificationType.RESERVATION_CANCELED);
            if (userSettings.showHostGraded)
                types.Add(NotificationType.HOST_GRADED);
            if (userSettings.showAccomodationGraded)
                types.Add(NotificationType.ACCOMODATION_GRADED);
            if (userSettings.showSuperHost)
                types.Add(NotificationType.SUPER_HOST);
            if (userSettings.showReservationRequestReply)
                types.Add(NotificationType.RESERVATION_REQUEST_REPLY);

            List<Notification> allNotifications = await _repository.GetAllByUserAsync(id);
            List<Notification> filteredNotifications = (List<Notification>)allNotifications.Where(n => types.Contains(n.Type));

            return filteredNotifications;
        }

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