using Microsoft.Extensions.Configuration;
using Resend;
using System.Net.Mail;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.RequestModels;
using TaskManager.SharedLayer.ResponseModel;

namespace TaskManager.SharedLayer.Immplementaion
{
    public class EmailService : IEmailService
    {
        private readonly IResend _resend;
        private readonly IConfiguration _configuration;
        public EmailService(IResend resend)
        {
            _resend = resend;
        }

        public async Task<ResponseModel<bool>> SendEmail(SendEmailRequest emailRequest)
        {
            if (string.IsNullOrWhiteSpace(emailRequest.From))
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "SenderEmailMustNotBeNull"
                };

            if (string.IsNullOrWhiteSpace(emailRequest.To))
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "TargetEmailMustNotBeNull"
                };

            if (string.IsNullOrWhiteSpace(emailRequest.Subject))
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "SubjectMustNotBeNull"
                };

            if (string.IsNullOrWhiteSpace(emailRequest.Html))
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "HtmlMustNotBeNull"
                };

            //if (!IsValidEmail(emailRequest.From))
            //    return new ResponseModel<bool>
            //    {
            //        Success = false,
            //        Message = "SenderEmailFormatNotCorrect"
            //    };

            if (!IsValidEmail(emailRequest.To))
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "TargetEmailFormatNotCorrect"
                };

            var email = new EmailMessage
            {
                From = emailRequest.From,
                To = emailRequest.To,
                Subject = emailRequest.Subject,
                HtmlBody = emailRequest.Html
            };

            var response = await _resend.EmailSendAsync(email);

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = "EmailSentSuccessfully"
            };
        }



        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var address = new MailAddress(email);

                // Ensures the parsed address exactly matches the input.
                return address.Address.Equals(email, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
