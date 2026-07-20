using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Application.Handlers.IHandlers
{
    public interface IOTPHandler
    {
        Task<ResponseModel<OtpResponseDto>> HandleOtpValidation(ValidateOTPDto model);
    }
}
