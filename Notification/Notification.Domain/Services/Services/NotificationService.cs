using Microsoft.Extensions.Localization;
using Notification.Notification.Domain.IRepositories;
using Notification.Notification.Domain.IUnitOfWork;
using Notification.Notification.Domain.Models;
using Notification.Notification.Domain.Services.IServices;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Notification;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Notifications;

namespace Notification.Notification.Domain.Services.Services
{
    public class NotificationService(INotificationsModuleUoW _notificationsModuleUoW, INotificationRepository _notificationRepository, ICurrentUserService _currentUserService, IUserLookupService userLookupService, IStringLocalizer<SharedResource> _localizer, IUserLookupService _userLookupService) : INotificationService
    {
        public async Task<ResponseModel<bool>> AddNewBulkCustomNotification(NewCustomBulkNotificationDTO model)
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
                    null,
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
        public async Task<ResponseModel<bool>> SendCustomNotification(NewCustomNotificationDTO model)
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


            var newNotification = Notifications.Create(model.Title, model.Text, model.UserId, null, _currentUserService.UserId);

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

        public async Task<ResponseModel<bool>> DeleteBulkNotifications(DeleteBulkNotifications model)
        {

            var notificationInfo = await _notificationRepository.GetAllById(model.NotificationIds, x => x.Where(n => n.UserId == _currentUserService.UserId));

            if (notificationInfo == null)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["SomthingWentWrong"]
                };
            }

            foreach (var item in notificationInfo)
            {
                item.MarkAsDeleted(_currentUserService.UserId);
            }






            await _notificationsModuleUoW.SaveChangesAsync();




            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["NotificationDeletedSuccessfully"]
            };
        }

        public async Task<ResponseModel<bool>> DeleteNotification(DeleteNotificationDto model)
        {

            var notificationInfo = await _notificationRepository.GetById(model.NotificationId, x => x.Where(n => n.UserId == _currentUserService.UserId));

            if (notificationInfo == null)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["SomthingWentWrong"]
                };
            }
            var result = notificationInfo.MarkAsDeleted(_currentUserService.UserId);

            if (!result.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer[result.Error]
                };


            await _notificationsModuleUoW.SaveChangesAsync();




            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["NotificationDeletedSuccessfully"]
            };

        }



        public async Task<ResponseModel<PagedResult<GetNotificationInfoDTO>>> GetAllNotificationsByUserId(GetNotificationForUserDTO model)
        {
            var IsUserExists = await userLookupService.GetUserByIdAsync(_currentUserService.UserId);

            if (IsUserExists.IsDeleted || !IsUserExists.IsActive)
            {
                return new ResponseModel<PagedResult<GetNotificationInfoDTO>>
                {
                    Success = true,

                    Message = _localizer["UserNotFound"]
                };

            }

            var tasksResult = await _notificationRepository.GetNotificationaByUserIdAsync(model, _currentUserService.UserId);

            return new ResponseModel<PagedResult<GetNotificationInfoDTO>>
            {
                Success = true,
                Data = tasksResult,
                Message = tasksResult != null
        ? _localizer["DataRetunedSuccssefully"]
        : _localizer["NoDataFound"]
            };

        }


        public async Task<ResponseModel<bool>> MarkAllNotificaitionAsRead()
        {

            var notificationInfo = await _notificationRepository.GetAllNotificationaByUserIdAsync(_currentUserService.UserId, x => x.Where(r => r.IsRead != true), true);

            if (notificationInfo == null || !notificationInfo.Any())
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["SomthingWentWrong"]
                };
            }

            foreach (var item in notificationInfo)
            {
                item.MarkAllAsRead();
            }

            await _notificationsModuleUoW.SaveChangesAsync();


            return new ResponseModel<bool>
            {
                Success = true,

                Message = _localizer["OperationDone"]
            };

        }

        public async Task<ResponseModel<bool>> MarkOneNotificaitionAsRead(MarkAsReadRequestDTO model)
        {

            var notificationInfo = await _notificationRepository.GetById(model.NotificationId);


            if (notificationInfo.UserId != _currentUserService.UserId)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["SomthingWentWrong"]
                };
            }


            var result = notificationInfo.MarkAsRead();

            if (!result.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer[result.Error]
                };


            await _notificationsModuleUoW.SaveChangesAsync();




            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["NotificationMarkedReadSuccessfully"]
            };
        }



    }
}
