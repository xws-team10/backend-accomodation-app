using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace account_service.Model
{
    public class HostGrade
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid HostId { get; set; }
        public string GuestUsername { get; set; }
        public int Value { get; set; }
        public DateTime Created { get; set; }

        public HostGrade()
        {
            Created = DateTime.Now;
        }

        public bool Validate()
        {
            return Value >= 1 && Value <= 5;
        }
    }
}
