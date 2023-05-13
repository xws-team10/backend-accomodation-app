using Grpc.Net.Client;
using reservation_service.Model;
using search_service;

namespace reservation_service.GrpcServices
{
    public class SearchClient : ISearchClient
    {
        private readonly IConfiguration _configuration;
        public SearchClient(IConfiguration configuration) {
            _configuration = configuration;
        }
        public IEnumerable<ReservationRequest> ReturnAllAccomodations(Guid id)
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcSearch"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcSearch"]);
            var client = new GrpcSearch.GrpcSearchClient(channel);
            var request = new GetAllRequest { Id = id.ToString() };

            try
            {
                var reply = client.GetAllAccom(request);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}
