using Grpc.Core;
using notification_service.Model;
using notification_service.Service;

namespace notification_service.ProtoServices
{
    public class GrpcAccountNotificationService : GrpcAccountNotifications.GrpcAccountNotificationsBase
    {
        private readonly NotificationService _notificationService;
        public GrpcAccountNotificationService(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public override async Task<AccountNotificationResponse> SendNotification(AccountNotificationRequest request, ServerCallContext context)
        {
            var response = new AccountNotificationResponse();

            Notification notification = new Notification
            {
                Text = request.Text,
                UserId = new Guid(request.UserId),
                Type = (NotificationType)request.Type
            };

            await _notificationService.CreateAsync(notification);

            response.Created = true;

            return await Task.FromResult(response);
        }
    }
}
