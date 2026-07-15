namespace TaskManager.SharedLayer.RequestModels.Identity
{
    public class SendNewOtpDto
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Username { get; set; }
    }
}
