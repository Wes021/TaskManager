using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Domain.Services.IServices
{
    public interface IGenerateOtpService
    {
        Task<ResponseModel<bool>> GenerateNewOtp(SendNewOtpDto model);


        Task<ResponseModel<OtpResponseDto>> ValidateOtp(ValidateOTPDto model);


    }
}
