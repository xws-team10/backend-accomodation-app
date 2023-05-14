using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace account_service.Model
{
    public class UserAddress : Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}