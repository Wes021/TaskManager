using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.ResponseModels;
using static TaskManager.SharedLayer.Enums.SystemEnums;

namespace Notification.Notification.Domain.Models
{
    public class Notifications : IEntity, IAuditedEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int TargetId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; private set; }
        public NotificationType Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }



        public static GenericDomainResponseModel<Notifications> Create(string title, string text, int userId, int targetId, int createdUser
         )
        {

            if (string.IsNullOrWhiteSpace(title))
                return GenericDomainResponseModel<Notifications>.Fail("TitleRequired");

            if (string.IsNullOrWhiteSpace(text))
                return GenericDomainResponseModel<Notifications>.Fail("textRequired");

            if (userId <= 0)
                return GenericDomainResponseModel<Notifications>.Fail("InvalidUser");






            var task = new Notifications
            {
                Title = title,
                Text = text,
                UserId = userId,
                TargetId = targetId,
                IsRead = false,
                CreatedUser = createdUser,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                IsActive = true

            };

            return GenericDomainResponseModel<Notifications>.Success(task);

        }

    }
}
