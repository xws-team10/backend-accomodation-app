namespace FlyMateAPI.Core.Model
{
    public class Address
    {
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string StreetNumber { get; set; } = null!;
        public int PostalCode { get; set; }
    }
}