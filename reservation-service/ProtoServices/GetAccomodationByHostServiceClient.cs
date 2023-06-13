using AutoMapper;
using Grpc.Net.Client;

namespace reservation_service.ProtoServices
{
    public class GetAccomodationByHostServiceClient
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
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcGetAccomodationsByHost"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcGetAccomodationsByHost"]);
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
