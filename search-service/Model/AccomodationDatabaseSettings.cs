namespace search_service.Model
{
    public class AccomodationDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string AccomodationsCollectionName { get; set; } = null!;
    }
}
