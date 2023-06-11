using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace accomodation_service.Model
{
    public class AccomodationGrade
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid AccomodationId { get; set; }
        public string GuestUsername { get; set; }
        public int Value { get; set; }
        public DateTime Created { get; set; }

        public AccomodationGrade()
        {
            Created = DateTime.Now;
        }

        public bool Validate()
        {
            return Value >= 1 && Value <= 5;
        }
        
    }
}
