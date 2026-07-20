using Identity.Identity.Application.Handlers.IHandlers;
using Identity.Identity.Domain.Services.IServices;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Application.Handlers.Handlers
{
    public class OTPHandler(IGenerateOtpService _generateOtpService) : IOTPHandler
    {


        public async Task<ResponseModel<OtpResponseDto>> HandleOtpValidation(ValidateOTPDto model)
        {
            var result = await _generateOtpService.ValidateOtp(model);

            return result;
        }
    }
}
