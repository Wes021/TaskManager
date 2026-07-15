using TaskManager.SharedLayer.Interfaces;

namespace Identity.Identity.Domain.Models
{
    public class GeneratedOTP : IEntity, IAuditedEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string HashedOTP { get; set; }
        public int Attempts { get; set; }

        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }


        private GeneratedOTP() { }


        internal GeneratedOTP(int userId, string hashedOtp, int createdUser, DateTime expiresAt)
        {
            UserId = userId;
            HashedOTP = hashedOtp;
            Attempts = 0;
            CreatedDate = DateTime.Now;
            CreatedUser = createdUser;
            IsDeleted = false;
            IsActive = false;
            ExpiresAt = expiresAt;

        }


    }
}
