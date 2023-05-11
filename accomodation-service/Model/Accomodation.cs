using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace accomodation_service.Model
{
    public class Accomodation
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }                
        [BsonRepresentation(BsonType.String)]
        public Guid HostId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public int Price { get; set; }
        public int MinCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public Address Address { get; set; }
        public string PictureUrl { get; set; }
        public DateTime AvailableFromDate { get; set; }
        public DateTime AvailableToDate { get; set; }

        //availableFromDate
        //availableToDate
        //hostId

        // id dateFrom dateTo accomodationId 
        public bool AvailabilityValidate()
        {
            if (AvailableFromDate < DateTime.Now || AvailableToDate < AvailableFromDate) return false;   
            
            return true;
        }
        
    }
}
