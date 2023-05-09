namespace search_service.Model
{
    public class Accomodation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Capacity { get; set; }
        public Address Address { get; set; }
    }
}
