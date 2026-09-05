namespace TaskManager.SharedLayer.RequestModels.Notification
{
    public class NewCustomBulkNotificationDTO
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public List<int> UserIds { get; set; }


    }
}
