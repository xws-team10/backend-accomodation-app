using AutoMapper;
using Grpc.Net.Client;
using reservation_service;
using search_service.DTO;

namespace search_service.ProtoServices
{
    public class FreeAccomodations : IFreeAccomodations
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public FreeAccomodations(IConfiguration configuration, IMapper mapper)
        {
            this._configuration = configuration;
            this._mapper = mapper;
        }  
        public IEnumerable<AccomodationDto> GetAllFreeAccomodations(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcSearch"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcSearch"]);
            var client = new GrpcSearch.GrpcSearchClient(channel);
            var request = new GetAllRequest { StartDate = startDate.ToString(),EndDate = endDate.ToString() };

            try
            {
                var reply = client.GetAllFreeAccomodations(request);
                return _mapper.Map<IEnumerable<AccomodationDto>>(reply.Accomodation); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
            return null;
        }
    }
}
