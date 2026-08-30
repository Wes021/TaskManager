using Microsoft.Extensions.Localization;
using Notification.Notification.Domain.IRepositories;
using Notification.Notification.Domain.Services.IServices;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Notification;
using TaskManager.SharedLayer.ResponseModel;

namespace Notification.Notification.Domain.Services.Services
{
    public class InternalNotificationService(INotificationRepository _notificationRepository, IStringLocalizer<SharedResource> _localizer, IUserLookupService _userLookupService) : IInternalNotificationService
    {
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





            throw new NotImplementedException();
        }
    }
}
