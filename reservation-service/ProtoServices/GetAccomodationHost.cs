using accomodation_service;
using AutoMapper;
using Grpc.Net.Client;

namespace reservation_service.ProtoServices
{
    public class GetAccomodationHost : IGetAccomodationHost
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GetAccomodationHost(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public AccomodationHostResponse GetHost(Guid id)
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcGetAccomodationHost"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcGetAccomodationHost"]);
            var client = new GrpcGetAccomodationHost.GrpcGetAccomodationHostClient(channel);
            var request = new AccomodationHostRequest { Id = id.ToString()};

            try
            {
                var reply = client.GetAccomodationHost(request);
                return reply;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}
