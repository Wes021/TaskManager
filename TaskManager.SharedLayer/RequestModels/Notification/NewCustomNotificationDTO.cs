namespace TaskManager.SharedLayer.RequestModels.Notification
{
    public class NewCustomNotificationDTO
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public int UserId { get; set; }
    }
}
