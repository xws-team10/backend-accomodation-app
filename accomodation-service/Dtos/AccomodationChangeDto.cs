using accomodation_service.Model;

namespace accomodation_service.Dtos
{
    public class AccomodationChangeDto
    {
        public Guid Id { get; set; }
        public DateTime AvailableFromDate { get; set; }
        public DateTime AvailableToDate { get; set; }
    }
}