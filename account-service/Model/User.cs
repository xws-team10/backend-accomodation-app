using AspNetCore.Identity.MongoDbCore.Models;

namespace FlyMateAPI.Core.Model
{

    public class User : MongoIdentityUser<Guid>
    {
        public UserAddress Address { get; set; } = null!;
        public string UserRole { get; set; }
    }
}