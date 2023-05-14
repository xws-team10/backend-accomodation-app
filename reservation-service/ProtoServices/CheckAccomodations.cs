using AutoMapper;
using Grpc.Net.Client;

namespace reservation_service.ProtoServices
{
    public class CheckAccomodations : ICheckAccomodations
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CheckAccomodations(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public bool CheckAccomodadtions(Guid id, DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcCheckAccomodations"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcCheckAccomodations"]);
            var client = new GrpcCheckAccomodations.GrpcCheckAccomodationsClient(channel);
            var request = new CheckAccomodationsRequest { Id = id.ToString(), StartDate = startDate.ToString(), EndDate = endDate.ToString() };

            try
            {
                var reply = client.CheckAccomodations(request);
                return reply.IsFree;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return false;
            }
        }
    }
}
