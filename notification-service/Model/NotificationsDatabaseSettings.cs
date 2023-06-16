namespace notification_service.Model
{
    public class NotificationsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string NotificationsCollectionName { get; set; } = null!;
    }
}
