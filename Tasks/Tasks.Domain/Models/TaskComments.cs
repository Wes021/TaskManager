using System.ComponentModel.DataAnnotations;
using TaskManager.SharedLayer.Interfaces;

namespace Tasks.Tasks.Domain.Models
{
    public class TaskComments : IEntity, IAuditedEntity
    {


        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public int TasksId { get; set; }
        public Tasks Tasks { get; set; }


        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }


        private TaskComments() { }

        internal TaskComments(int taskId, string text, int createdUser)
        {
            Text = text;
            TasksId = taskId;
            CreatedUser = createdUser;
        }
    }
}
