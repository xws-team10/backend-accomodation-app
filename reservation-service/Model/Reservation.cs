namespace reservation_service.Model
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public Guid AccomodationId { get; set; }
        public Guid GuestId { get; set; }
    }
}
