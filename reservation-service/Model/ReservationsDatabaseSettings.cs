namespace reservation_service.Model
{
    public class ReservationsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ReservationsCollectionName { get; set; } = null!;
        public string ReservationRequestsCollectionName { get; set; } = null!;
    }
}
