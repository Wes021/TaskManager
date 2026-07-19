using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Domain.Services.IServices
{
    public interface IOtpService
    {


        Task<ResponseModel<GeneratedOtpDto>> GenerateOtp(int length);


        Task<ResponseModel<string>> OtpHasher(string otp);


        Task<ResponseModel<bool>> Verify(string OTP, string hashOtp);
    }
}
