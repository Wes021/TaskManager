using static TaskManager.SharedLayer.Enums.SystemEnums;

namespace TaskManager.SharedLayer.ResponseModels.Notifications
{
    public class GetNotificationInfoDTO
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int? TargetId { get; set; }
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
