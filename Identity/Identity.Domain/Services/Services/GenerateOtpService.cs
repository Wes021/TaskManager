using Identity.Identity.Domain.IRepositories;
using Identity.Identity.Domain.IUnitOfWork;
using Identity.Identity.Domain.Models;
using Identity.Identity.Domain.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Module.Identity.Domain.IRepositories;
using Module.Identity.Domain.Services.IServices;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;


namespace Identity.Identity.Domain.Services.Services
{
    public class GenerateOtpService(IIdentityMouduleUoW _UoW, IEmailService _emailService,
        IGeneratedOTPRepository _generatedOTPRepository, IUserRepository _userRepository,
        IStringLocalizer<SharedResource> _localizer, IOtpService _otpService, IConfiguration _configuration,
        ICurrentUserService _currentUserService, IJwtService _jwtService) : IGenerateOtpService

    {



        public async Task<ResponseModel<bool>> GenerateNewOtp(SendNewOtpDto model)
        {
            var user = await _userRepository.GetByEmail(model.Email, null, false);

            if (user is null)
                return new ResponseModel<bool> { Success = false, Message = _localizer["UserNotFound"] };

            if (!user.IsAccountDeleted())
                return new ResponseModel<bool> { Success = false, Message = _localizer["SomthingWentWrong"] };

            if (!user.IsAccountActive())
                return new ResponseModel<bool> { Success = false, Message = _localizer["YourActounIsNotActive"] };

            var OtpLength = _configuration.GetValue<int>("OtpSettings:OtpLength");
            var generatedOtp = await _otpService.GenerateOtp(OtpLength);

            if (!generatedOtp.Success)
                return new ResponseModel<bool> { Success = false, Message = _localizer["SomthingWentWrong"] };

            var HashedOTP = await _otpService.OtpHasher(generatedOtp.Data.OtpNumber);

            var NewOtp = GeneratedOTP.Create(user.Id, HashedOTP.Data, user.Id, DateTime.Now.AddSeconds(_configuration.GetValue<int>("OtpSettings:OtpExpireInSecounds")));

            await _generatedOTPRepository.Add(NewOtp);

            await _UoW.SaveChangesAsync();

            var EmailResponse = await _emailService.SendEmail(new SendEmailRequest
            {
                From = _configuration.GetValue<string>("SenderEmailAddress:SenderEmail"),
                To = _configuration.GetValue<string>("SenderEmailAddress:ReciverEmail"),
                Subject = "Your OTP Code",
                Html = $@"
        <h2>Task Manager</h2>
        <p>Your one-time password is:</p>
        <h1 style='letter-spacing:5px;'>{generatedOtp.Data.OtpNumber}</h1>
        <p>This code expires in 5 minutes.</p>"
            });

            if (!EmailResponse.Success)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = _localizer["EmailFaildToSend"]
                };


            return new ResponseModel<bool>
            {
                Success = true,
                Message = _localizer["OTPSendSuccessfully"]
            };
        }

        public async Task<ResponseModel<OtpResponseDto>> ValidateOtp(ValidateOTPDto model)
        {
            var user = await _userRepository.GetByEmail(model.Email, null, false);

            if (user is null)
                return new ResponseModel<OtpResponseDto> { Success = false, Message = _localizer["UserNotFound"] };

            if (!user.IsAccountDeleted())
                return new ResponseModel<OtpResponseDto> { Success = false, Message = _localizer["SomthingWentWrong"] };

            if (!user.IsAccountActive())
                return new ResponseModel<OtpResponseDto> { Success = false, Message = _localizer["YourActounIsNotActive"] };

            var HashedOtp = await _generatedOTPRepository.GetGeneratedOTPAsync(new GetOtpSearchDto { UserId = user.Id });

            if (HashedOtp is null)
                return new ResponseModel<OtpResponseDto> { Success = false, Message = _localizer["OtpIsWrongPleaseCheckAgainOrTryAgainLater"] };

            var verificatonResult = await _otpService.VerifyOtp(model.OTP, HashedOtp.HashedOTP);

            if (!verificatonResult.Data)
            {

                HashedOtp.Attempts++;
                return new ResponseModel<OtpResponseDto> { Success = false, Message = _localizer["OtpIsWrongPleaseCheckAgainOrTryAgainLater"] };


            }

            HashedOtp.Attempts++;
            HashedOtp.IsDeleted = true;
            HashedOtp.IsActive = false;



            return new ResponseModel<OtpResponseDto> { Data = new OtpResponseDto { resetToken = _jwtService.GenerateResetPasswordToken(user.Id) }, Success = true };


        }


    }
}
