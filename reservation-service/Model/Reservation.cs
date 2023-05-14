using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace reservation_service.Model
{
    public class Reservation
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid AccomodationId { get; set; }
        public string GuestUsername { get; set; }

        public Reservation()
        {
            Created = DateTime.Now;
        }

        public bool Validate()
        {
            if (StartDate < DateTime.Now || EndDate < StartDate) return false;
            if (NumberOfGuests < 1) return false;
            
            return true;
        }

        public bool Overlaps(Reservation other) => (StartDate <= other.EndDate) && (EndDate >= other.StartDate);
        public bool Overlaps(ReservationRequest other) => (StartDate <= other.EndDate) && (EndDate >= other.StartDate);

        public bool Overlaps(DateTime startDate, DateTime endDate) => (StartDate <= endDate) && (EndDate >= startDate);
    }
}
