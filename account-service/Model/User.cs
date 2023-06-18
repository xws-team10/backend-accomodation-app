using AspNetCore.Identity.MongoDbCore.Models;

namespace account_service.Model
{

    public class User : MongoIdentityUser<Guid>
    {
        public string Address { get; set; } = null!;
        public string UserRole { get; set; }
        public int CancelCounterGuest { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public float Rating { get; set; }
    }
}