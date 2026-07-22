namespace TaskManager.SharedLayer.RequestModels.Identity
{
    public class UpdateUserPassword
    {
        public string ResetPasswordToken { get; set; }

        public string Email { get; set; }

        public string password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
