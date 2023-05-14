using accomodation_service.Dtos;
using AutoMapper;
using Grpc.Net.Client;
using reservation_service;
using search_service;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

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

        public bool CheckReservations(Guid id, DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcCheckReservations"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcCheckReservations"]);
            var client = new GrpcCheckReservations.GrpcCheckReservationsClient(channel);
            var request = new CheckReservationsRequest { Id = id.ToString(), StartDate = startDate.ToString(), EndDate = endDate.ToString()};

            try
            {
                var reply = client.CheckReservations(request);
                return reply.IsFree;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return false;
            }
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

        public void UpdateAccomodationPrice(AccomodationChangePriceDto changePriceDto)
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcCreate"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcCreate"]);
            var client = new GrpcCreate.GrpcCreateClient(channel);
            var request = new UpdatePriceRequest { Id = changePriceDto.Id.ToString(), Price = changePriceDto.Price };
            try
            {
                var reply = client.UpdateAccomodationPrice(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
            }
        }
    }
}
