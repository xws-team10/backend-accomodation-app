using AspNetCore.Identity.MongoDbCore.Models;

namespace account_service.Model
{

    public class User : MongoIdentityUser<Guid>
    {
        public UserAddress Address { get; set; } = null!;
        public string UserRole { get; set; }
        public int CancelCounterGuest { get; set; }
    }
}