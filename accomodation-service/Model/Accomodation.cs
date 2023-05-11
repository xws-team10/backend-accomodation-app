using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace accomodation_service.Model
{
    public class Accomodation
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int minCapacity { get; set; }
        public int maxCapacity { get; set; }
        public Address Address { get; set; }
        public string PictureUrl { get; set; }
    }
}
