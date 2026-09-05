using Microsoft.Extensions.Localization;
using Notification.Notification.Domain.IRepositories;
using Notification.Notification.Domain.IUnitOfWork;
using Notification.Notification.Domain.Models;
using Notification.Notification.Domain.Services.IServices;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Notification;
using TaskManager.SharedLayer.ResponseModel;

namespace Notification.Notification.Domain.Services.Services
{
    public class InternalNotificationService(INotificationRepository _notificationRepository, INotificationsModuleUoW _notificationsModuleUoW, IStringLocalizer<SharedResource> _localizer, IUserLookupService _userLookupService, ICurrentUserService _currentUserService) : IInternalNotificationService
    {
        public async Task<ResponseModel<bool>> AddNewBulkInternalNotification(
    NewInternalBulkNotificationDTO model)
        {
            var users = await _userLookupService.GetUsersByIdsAsync(model.UserIds);

            if (users is null || !users.Any())
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotFound"]
                };
            }

            foreach (var user in users)
            {
                var result = Notifications.Create(
                    model.Title,
                    model.Text,
                    user.Id,
                    model.TargetId,
                    _currentUserService.UserId);

                if (!result.Succeeded)
                {
                    return new ResponseModel<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = _localizer["SomthingWentWrong"]
                    };
                }

                await _notificationRepository.Add(result.Data);

            }

            await _notificationsModuleUoW.SaveChangesAsync();

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["NotificationAdded"]
            };
        }

        public async Task<ResponseModel<bool>> AddNewInternalNotification(NewInternalNotificationDTO model)
        {
            var user = await _userLookupService.GetUserByIdAsync(model.UserId);

            if (user is null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotFound"]
                };


            if (user.IsDeleted || !user.IsActive)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotFounf"]
                };


            var newNotification = Notifications.Create(model.Title, model.Text, model.UserId, model.TargetId, _currentUserService.UserId);

            await _notificationRepository.Add(newNotification.Data);

            await _notificationsModuleUoW.SaveChangesAsync();
            if (!newNotification.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["SomthingWentWrong"]
                };

            return new ResponseModel<bool>
            {
                Success = true,
                Data = false,
                Message = _localizer["NotificationAdded"]
            };
        }
    }
}
