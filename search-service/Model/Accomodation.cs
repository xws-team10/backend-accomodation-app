using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace search_service.Model
{
    public class Accomodation
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Capacity { get; set; }
        public Address Address { get; set; }
        public DateTime AvailableFromDate { get; set; }
        public DateTime AvailableToDate { get; set; }

        public bool AvailabilityInitialValidate()
        {
            if (AvailableFromDate < DateTime.Now || AvailableToDate < AvailableFromDate) return false;

            return true;
        }
    }
}
