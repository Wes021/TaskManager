using System.Security.Cryptography;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Domain.Services.Services
{
    public class OtpGenerator
    {
        public async Task<ResponseModel<GeneratedOtpDto>> GenerateOtp(int length)
        {
            var otp = new char[length];

            for (int i = 0; i < length; i++)
            {
                otp[i] = (char)('0' + RandomNumberGenerator.GetInt32(0, 10));
            }


        }
    }
}
