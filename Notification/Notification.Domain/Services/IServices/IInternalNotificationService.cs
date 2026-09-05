using TaskManager.SharedLayer.RequestModels.Notification;
using TaskManager.SharedLayer.ResponseModel;

namespace Notification.Notification.Domain.Services.IServices
{
    public interface IInternalNotificationService
    {
        Task<ResponseModel<bool>> AddNewInternalNotification(NewInternalNotificationDTO model);

        Task<ResponseModel<bool>> AddNewBulkInternalNotification(NewInternalBulkNotificationDTO model);
    }
}
