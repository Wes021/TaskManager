using Identity.Identity.Domain.Services.IServices;
using System.Security.Cryptography;
using System.Text;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Domain.Services.Services
{
    public class OtpService : IOtpService
    {
        private readonly byte[] _secret =
        Encoding.UTF8.GetBytes("your-secret-key");

        public async Task<ResponseModel<GeneratedOtpDto>> GenerateOtp(int length)
        {
            var otp = new char[length];

            for (int i = 0; i < length; i++)
            {
                otp[i] = (char)('0' + RandomNumberGenerator.GetInt32(0, 10));
            }


            return new ResponseModel<GeneratedOtpDto>
            {
                Data = new GeneratedOtpDto { OtpNumber = new string(otp) },
                Success = true
            };
        }

        public async Task<ResponseModel<string>> OtpHasher(string otp)
        {

            using var hmac = new HMACSHA256(_secret);

            var hash = hmac.ComputeHash(
            Encoding.UTF8.GetBytes(otp));

            return new ResponseModel<string>
            {
                Data = Convert.ToBase64String(hash),

                Success = true

            };

        }

        public async Task<ResponseModel<bool>> VerifyOtp(string OTP, string hashOtp)
        {
            var computedHash = await OtpHasher(OTP);


            var result = CryptographicOperations.FixedTimeEquals(
              Convert.FromBase64String(computedHash.Data),
              Convert.FromBase64String(hashOtp));

            return new ResponseModel<bool>
            {
                Data = result

            };
        }
    }
}
