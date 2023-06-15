namespace account_service.Model
{
    public class AccountDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string AccountCollectionName { get; set; } = null!;
        public string AccountRequestsCollectionName { get; set; } = null!;
        public string HostGradesCollectionName { get; set; } = null!;
    }
}
