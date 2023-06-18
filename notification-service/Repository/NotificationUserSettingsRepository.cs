using Microsoft.Extensions.Options;
using MongoDB.Driver;
using notification_service.Model;
using notification_service.Repository.Core;

namespace notification_service.Repository
{
    public class NotificationUserSettingsRepository : INotificationUserSettingsRepository
    {
        private readonly IMongoCollection<NotificationUserSettings> _notificationUserSettingsCollection;

        public NotificationUserSettingsRepository(IOptions<NotificationsDatabaseSettings> notificationsDatabaseSettings)
        {
            var mongoClient = new MongoClient(notificationsDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(notificationsDatabaseSettings.Value.DatabaseName);
            _notificationUserSettingsCollection = mongoDatabase.GetCollection<NotificationUserSettings>(notificationsDatabaseSettings.Value.NotificationUserSettingsCollectionName);
        }
        public async Task<List<NotificationUserSettings>> GetAllAsync() =>
            await _notificationUserSettingsCollection.Find(_ => true).ToListAsync();

        public async Task<NotificationUserSettings> GetByIdAsync(Guid id) =>
            await _notificationUserSettingsCollection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

        public async Task<NotificationUserSettings> GetByUserAsync(Guid id) =>
            await _notificationUserSettingsCollection.Find(x => x.UserId.Equals(id)).FirstOrDefaultAsync();

        public async Task CreateAsync(NotificationUserSettings newNotificationUserSettings) =>
            await _notificationUserSettingsCollection.InsertOneAsync(newNotificationUserSettings);

        public async Task UpdateAsync(Guid id, NotificationUserSettings updateNotificationUserSettings) =>
            await _notificationUserSettingsCollection.ReplaceOneAsync(x => x.Id.Equals(id), updateNotificationUserSettings);

        public async Task DeleteAsync(Guid id) =>
            await _notificationUserSettingsCollection.DeleteOneAsync(x => x.Id.Equals(id));
    }
}
