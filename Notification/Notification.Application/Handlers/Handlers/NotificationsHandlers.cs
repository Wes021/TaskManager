using Microsoft.Extensions.Localization;
using Notification.Notification.Application.Handlers.IHandlers;
using Notification.Notification.Domain.Services.IServices;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Notification;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Notifications;

namespace Notification.Notification.Application.Handlers.Handlers
{
    public class NotificationsHandlers(IStringLocalizer<SharedResource> _localizer, INotificationService _notificationService) : INotificationsHandlers
    {
        public async Task<ResponseModel<bool>> AddNewBulkNotification(NewCustomBulkNotificationDTO model)
        {
            if (model.UserIds.Count <= 0 || String.IsNullOrWhiteSpace(model.Text) || String.IsNullOrWhiteSpace(model.Title))
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = _localizer["InvalidRequest"]
                };


            var request = await _notificationService.AddNewBulkCustomNotification(model);

            return request;
        }

        public async Task<ResponseModel<bool>> AddNewNotification(NewCustomNotificationDTO model)
        {
            if (model.UserId <= 0 || String.IsNullOrWhiteSpace(model.Text) || String.IsNullOrWhiteSpace(model.Title))
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = _localizer["InvalidRequest"]
                };

            var request = await _notificationService.SendCustomNotification(model);

            return request;


        }

        public async Task<ResponseModel<bool>> DeleteNotification(DeleteNotificationDto model)
        {
            if (model.NotificationId <= 0)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = _localizer["InvalidRequest"]
                };

            var request = await _notificationService.DeleteNotification(model);

            return request;
        }

        public async Task<ResponseModel<bool>> DeleteBulkNotification(DeleteBulkNotifications model)
        {
            if (model.NotificationIds.Count <= 0)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = _localizer["InvalidRequest"]
                };

            var request = await _notificationService.DeleteBulkNotifications(model);

            return request;
        }

        public async Task<ResponseModel<bool>> MarkOneNotificaitionAsRead(MarkAsReadRequestDTO model)
        {
            if (model.NotificationId <= 0)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = _localizer["InvalidRequest"]
                };

            var request = await _notificationService.MarkOneNotificaitionAsRead(model);

            return request;
        }

        public async Task<ResponseModel<bool>> MarkBulkNotificaitionAsRead()
        {

            var request = await _notificationService.MarkAllNotificaitionAsRead();

            return request;
        }

        public async Task<ResponseModel<PagedResult<GetNotificationInfoDTO>>> GetAllNotificationsByUserId(GetNotificationForUserDTO model)
        {
            if (model == null)
                return new ResponseModel<PagedResult<GetNotificationInfoDTO>>
                {
                    Success = false,
                    Message = _localizer["InvalidRequest"]
                };

            var response = await _notificationService.GetAllNotificationsByUserId(model);

            return response;
        }
    }
}
