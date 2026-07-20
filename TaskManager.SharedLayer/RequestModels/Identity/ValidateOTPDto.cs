namespace TaskManager.SharedLayer.RequestModels.Identity
{
    public class ValidateOTPDto
    {
        public string Email { get; set; }

        public string OTP { get; set; }
    }
}
