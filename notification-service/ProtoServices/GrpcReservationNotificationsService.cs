using Grpc.Core;
using notification_service.Model;
using notification_service.Service;

namespace notification_service.ProtoServices
{
    public class GrpcReservationNotificationsService : GrpcReservationNotifications.GrpcReservationNotificationsBase
    {
        private readonly NotificationService _notificationService;
        public GrpcReservationNotificationsService(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public override async Task<NotificationResponse> SendNotification(NotificationRequest request, ServerCallContext context)
        {
            var response = new NotificationResponse();

            Notification notification = new Notification
            {
                Text = request.Text,
                UserId = new Guid(request.UserId),
                Type = (NotificationType)request.Type
            };

            if (notification.Type.Equals(NotificationType.SUPER_HOST))
            {
                List<Notification> allNotifications = await _notificationService.GetAllByUserAsync(notification.UserId);
                List<Notification> filteredNotifications = allNotifications.FindAll(n => n.Type.Equals(NotificationType.SUPER_HOST)).OrderByDescending(n => n.Created).ToList();

                if (filteredNotifications.Count > 0)
                {
                    if (!filteredNotifications[0].Text.Equals(request.Text))
                    {
                        await _notificationService.CreateAsync(notification);
                        response.Created = true;
                        return await Task.FromResult(response);
                    }
                    else
                    {
                        return await Task.FromResult(response);
                    }
                }
            }

            await _notificationService.CreateAsync(notification);

            response.Created = true;

            return await Task.FromResult(response);
        }
    }
}
