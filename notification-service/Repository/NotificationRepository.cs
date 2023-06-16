using Microsoft.Extensions.Options;
using MongoDB.Driver;
using notification_service.Model;
using notification_service.Repository.Core;

namespace notification_service.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoCollection<Notification> _notificationsCollection;

        public NotificationRepository(IOptions<NotificationsDatabaseSettings> notificationsDatabaseSettings)
        {
            var mongoClient = new MongoClient(notificationsDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(notificationsDatabaseSettings.Value.DatabaseName);
            _notificationsCollection = mongoDatabase.GetCollection<Notification>(notificationsDatabaseSettings.Value.NotificationsCollectionName);
        }
        public async Task<List<Notification>> GetAllAsync() =>
            await _notificationsCollection.Find(_ => true).ToListAsync();

        public async Task<Notification> GetByIdAsync(Guid id) =>
            await _notificationsCollection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

        public async Task<List<Notification>> GetAllByUserAsync(Guid id) =>
            await _notificationsCollection.Find(x => x.UserId.Equals(id)).ToListAsync();

        public async Task CreateAsync(Notification newNotification) =>
            await _notificationsCollection.InsertOneAsync(newNotification);

        public async Task UpdateAsync(Guid id, Notification updateNotification) =>
            await _notificationsCollection.ReplaceOneAsync(x => x.Id.Equals(id), updateNotification);

        public async Task DeleteAsync(Guid id) =>
            await _notificationsCollection.DeleteOneAsync(x => x.Id.Equals(id));
    }
}
