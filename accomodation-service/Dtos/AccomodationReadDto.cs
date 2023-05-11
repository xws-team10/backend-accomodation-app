using accomodation_service.Model;

namespace accomodation_service.Dtos
{
    public class AccomodationReadDto
    {
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