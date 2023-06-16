using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace notification_service.Model
{
    public class NotificationUserSettings
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; }
        public bool showReservationRequestCreated { get; set; }
        public bool showReservationCanceled { get; set; }
        public bool showHostGraded { get; set; }
        public bool showAccomodationGraded { get; set; }
        public bool showSuperHost { get; set; }
        public bool showReservationRequestReply { get; set; }

        public NotificationUserSettings()
        {
            showReservationRequestCreated = true;
            showReservationCanceled = true;
            showHostGraded = true;
            showAccomodationGraded = true;
            showSuperHost = true;
            showReservationRequestReply = true;
        }
    }
}
