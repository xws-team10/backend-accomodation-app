using accomodation_service.Model;

namespace accomodation_service.Dtos
{
    public class AccomodationChangePriceDto
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
    }
}