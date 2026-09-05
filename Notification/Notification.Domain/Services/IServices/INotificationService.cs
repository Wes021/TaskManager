using TaskManager.SharedLayer.RequestModels.Notification;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Notifications;

namespace Notification.Notification.Domain.Services.IServices
{
    public interface INotificationService
    {
        Task<ResponseModel<PagedResult<GetNotificationInfoDTO>>> GetAllNotificationsByUserId(GetNotificationForUserDTO model);

        Task<ResponseModel<bool>> MarkOneNotificaitionAsRead(MarkAsReadRequestDTO model);


        Task<ResponseModel<bool>> MarkAllNotificaitionAsRead();

        Task<ResponseModel<bool>> SendCustomNotification(NewCustomNotificationDTO model);

        Task<ResponseModel<bool>> AddNewBulkCustomNotification(
    NewCustomBulkNotificationDTO model);

        Task<ResponseModel<bool>> DeleteNotification(DeleteNotificationDto model);

        Task<ResponseModel<bool>> DeleteBulkNotifications(DeleteBulkNotifications model);

    }
}
