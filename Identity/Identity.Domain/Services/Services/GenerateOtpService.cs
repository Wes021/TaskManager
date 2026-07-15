using Identity.Identity.Domain.Services.IServices;
using Microsoft.Extensions.Localization;
using Module.Identity.Domain.IRepositories;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModel;

namespace Identity.Identity.Domain.Services.Services
{
    public class GenerateOtpService(IUserRepository _userRepository, IStringLocalizer<SharedResource> _localizer) : IGenerateOtpService
    {
        public async Task<ResponseModel<bool>> GenerateNewOtp(SendNewOtpDto model)
        {
            var user = await _userRepository.GetByEmail(model.Email, null, false);

            if (user is null)
                return new ResponseModel<bool> { Success = false, Message = _localizer["UserNotFount"] };

            if (!user.IsAccountDeleted())
                return new ResponseModel<bool> { Success = false, Message = _localizer["SomthingWentWrong"] };

            if (!user.IsAccountActive())
                return new ResponseModel<bool> { Success = false, Message = _localizer["YourActounIsNotActive"] };




            throw new NotImplementedException();
        }
    }
}
