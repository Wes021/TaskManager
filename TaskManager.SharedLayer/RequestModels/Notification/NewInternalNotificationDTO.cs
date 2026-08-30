namespace TaskManager.SharedLayer.RequestModels.Notification
{
    public class NewInternalNotificationDTO
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int TargetId { get; set; }
    }
}
