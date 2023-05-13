using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FlyMateAPI.Core.Model
{
    public class UserAddress : Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}