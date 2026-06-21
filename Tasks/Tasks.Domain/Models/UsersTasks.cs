using System.ComponentModel.DataAnnotations;
using TaskManager.SharedLayer.Interfaces;

namespace Tasks.Tasks.Domain.Models
{
    public class UsersTasks : IEntity, IAuditedEntity
    {
        [Key]
        public int Id { get; set; }


        public int TasksId { get; set; }
        public Tasks Tasks { get; set; }


        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        private UsersTasks() { }

        internal UsersTasks(int taskId, int userId, int? createdUser)
        {

            TasksId = taskId;
            UserId = userId;
            CreatedUser = createdUser;

            CreatedDate = DateTime.Now;
            IsDeleted = false;
            IsActive = true;
        }



        internal void DeleteOrRemove(int modifiedUser)
        {
            IsDeleted = true;

            IsActive = false;

            ModifiedDate = DateTime.UtcNow;

            ModifiedUser = modifiedUser;
        }


    }
}
