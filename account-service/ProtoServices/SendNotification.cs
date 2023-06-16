using AutoMapper;
using Grpc.Net.Client;
using notification_service;

namespace account_service.ProtoServices
{
    public class SendNotification : ISendNotification
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public SendNotification(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public bool CreateNotification(string text, Guid userId, int type)
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcAccountNotifications"]} ");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcAccountNotifications"]);
            var client = new GrpcAccountNotifications.GrpcAccountNotificationsClient(channel);
            var request = new AccountNotificationRequest { Text = text, UserId = userId.ToString(), Type = type };

            try
            {
                var reply = client.SendNotification(request);
                return reply.Created;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return false;
            }
        }
    }
}
