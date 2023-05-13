using accomodation_service.Model;

namespace accomodation_service.Dtos
{
    public class AccomodationCreateDto
    {
        public Guid HostId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public bool PricePerGuest { get; set; }
        public int Price { get; set; }
        public int MinCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public Address Address { get; set; }
        public string PictureUrl { get; set; }
        public DateTime AvailableFromDate { get; set; }
        public DateTime AvailableToDate { get; set; }
    
    }
}