using AutoMapper;
using Grpc.Net.Client;
using search_service;

namespace accomodation_service.ProtoServices
{
    public class CreateAccomodation : ICreateAccomodation
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CreateAccomodation(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public void CreateNewAccomodation(Guid Id, string Name, string Description, int Price, int Capacity, string Country, string City, string Street, string StreetNumber, DateTime AvailableFromDate, DateTime AvailableToDate)
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcCreate"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcCreate"]);
            var client = new GrpcCreate.GrpcCreateClient(channel);
            var request = new CreateRequest { Id = Id.ToString(), Name = Name, Description = Description, Price = Price, Capacity = Capacity, Country = Country, City = City, Street = Street, StreetNumber = StreetNumber, AvailableFromDate = AvailableFromDate.ToString(), AvailableToDate = AvailableToDate.ToString()  };

            try
            {
                var reply = client.CreateNewAccomodation(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
            }
        }

        public void UpdateAccomodation(Guid Id, DateTime AvailableFromDate, DateTime AvailableToDate)
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcCreate"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcCreate"]);
            var client = new GrpcCreate.GrpcCreateClient(channel);
            var request = new UpdateRequest { Id = Id.ToString(),AvailableFromDate= AvailableFromDate.ToString(),AvailableToDate = AvailableToDate.ToString() };
            try
            {
                var reply = client.UpdateAccomodation(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
            }
        }
    }
}
