namespace search_service.DTO
{
    public class AccomodationUpdateDto
    {
        public Guid Id { get; set; }
        public DateTime AvailableFromDate { get; set; }
        public DateTime AvailableToDate { get; set; }
    }
}
