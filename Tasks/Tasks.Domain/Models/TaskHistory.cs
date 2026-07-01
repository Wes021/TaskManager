using TaskManager.SharedLayer.Interfaces;

namespace Tasks.Tasks.Domain.Models
{
    public class TaskHistory : IEntity, IAuditedEntity
    {
        public int Id { get; set; }

        public string ActionDetails { get; set; }


        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;

        private TaskHistory() { }



        internal TaskHistory(int createdUser, string actionDetails)
        {
            CreatedUser = createdUser;
            ActionDetails = actionDetails;
            CreatedDate = DateTime.Now;

        }
    }
}
