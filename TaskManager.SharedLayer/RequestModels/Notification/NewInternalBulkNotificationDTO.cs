namespace TaskManager.SharedLayer.RequestModels.Notification
{
    public class NewInternalBulkNotificationDTO
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public List<int> UserIds { get; set; }

        public int TargetId { get; set; }
    }
}
