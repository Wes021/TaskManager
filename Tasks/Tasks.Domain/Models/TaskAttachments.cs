using System.ComponentModel.DataAnnotations;
using TaskManager.SharedLayer.Interfaces;

namespace Tasks.Tasks.Domain.Models
{
    public class TaskAttachments : IEntity, IAuditedEntity
    {
        [Key]
        public int Id { get; set; }


        public int TasksId { get; set; }
        public Tasks Tasks { get; set; }

        public string AttachmentType { get; set; }

        public string AttachmentName { get; set; }

        public string AttachmentPath { get; set; }

        public string AttachmentSize { get; set; }

        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }



        private TaskAttachments() { }

        internal TaskAttachments(int taskId, string attactmentType, string attacmentName, string attachementPath, string attachementSize, int? createdUser)
        {
            TasksId = taskId;
            AttachmentType = attactmentType;
            AttachmentName = attacmentName;
            CreatedUser = createdUser;
            CreatedDate = DateTime.Now;
            IsDeleted = false;
            IsActive = true;

        }
    }
}
