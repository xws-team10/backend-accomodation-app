using AutoMapper;
using Grpc.Net.Client;
using host_service;

namespace reservation_service.ProtoServices
{
    public class GetAccomodationByHostServiceClient : IGetAccomodationByHostServiceClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GetAccomodationByHostServiceClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public AccomodationsResponse GetAccommodationsByHostId(string hostId)
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcCheckAccomodations"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcCheckAccomodations"]);
            var client = new GrpcGetAccomodationsByHost.GrpcGetAccomodationsByHostClient(channel);
            var request = new HostIdRequest { HostId = hostId };

            try
            {
                var reply = client.GetAccomodationsByHostId(request);
                return reply;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}
