using account_service;
using AutoMapper;
using Grpc.Net.Client;

namespace reservation_service.ProtoServices
{
    public class GetUserId : IGetUserId
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GetUserId(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public Guid? GetUserByUsername(string username)
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcGetUserId"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcGetUserId"]);
            var client = new GrpcGetUserId.GrpcGetUserIdClient(channel);
            var request = new UserIdRequest { Username = username };

            try
            {
                var reply = client.GetUserId(request);
                return new Guid(reply.UserId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}
