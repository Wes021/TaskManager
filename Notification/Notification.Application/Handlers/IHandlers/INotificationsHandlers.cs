using TaskManager.SharedLayer.RequestModels.Notification;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Notifications;

namespace Notification.Notification.Application.Handlers.IHandlers
{
    public interface INotificationsHandlers
    {
        Task<ResponseModel<bool>> AddNewNotification(NewCustomNotificationDTO model);

        Task<ResponseModel<bool>> AddNewBulkNotification(NewCustomBulkNotificationDTO model);

        Task<ResponseModel<bool>> DeleteNotification(DeleteNotificationDto model);

        Task<ResponseModel<bool>> DeleteBulkNotification(DeleteBulkNotifications model);
        Task<ResponseModel<bool>> MarkOneNotificaitionAsRead(MarkAsReadRequestDTO model);

        Task<ResponseModel<PagedResult<GetNotificationInfoDTO>>> GetAllNotificationsByUserId(GetNotificationForUserDTO model);
        Task<ResponseModel<bool>> MarkBulkNotificaitionAsRead();
    }
}
