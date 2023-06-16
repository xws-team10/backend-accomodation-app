using Grpc.Core;
using notification_service.Model;
using notification_service.Service;

namespace notification_service.ProtoServices
{
    public class GrpcAccomodationNotificationsService : GrpcAccomodationNotifications.GrpcAccomodationNotificationsBase
    {
        private readonly NotificationService _notificationService;
        public GrpcAccomodationNotificationsService(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public override async Task<AccomodationNotificationResponse> SendNotification(AccomodationNotificationRequest request, ServerCallContext context)
        {
            var response = new AccomodationNotificationResponse();

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
