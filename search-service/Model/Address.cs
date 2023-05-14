namespace search_service.Model
{
    public class Address
    {
        public Address(string country, string city, string street, string streetNumber)
        {
            Country = country;
            City = city;
            Street = street;
            StreetNumber = streetNumber;
        }

        public Guid Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
    }
}
