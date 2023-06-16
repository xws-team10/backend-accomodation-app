using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace notification_service.Model
{
    public class Notification
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public NotificationType Type { get; set; }
        public string Text { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; }
        public bool IsRead { get; set; }

        public Notification()
        {
            Created = DateTime.Now;
        }
    }
}
