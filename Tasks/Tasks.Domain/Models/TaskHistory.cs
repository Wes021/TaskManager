using TaskManager.SharedLayer.Interfaces;
using static TaskManager.SharedLayer.Enums.SystemEnums;

namespace Tasks.Tasks.Domain.Models
{
    public class TaskHistory : IEntity, IAuditedEntity
    {
        public int Id { get; set; }

        public string ActionDetails { get; set; }

        public int TaskId { get; set; }
        public TaskHistoryAction Action { get; private set; }
        public string? Metadata { get; private set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;

        private TaskHistory() { }

        internal TaskHistory(
        int taskId,
        TaskHistoryAction action,
        int createdUser,
        string? metadata = null)
        {
            TaskId = taskId;
            Action = action;
            CreatedUser = createdUser;
            CreatedDate = DateTime.UtcNow;
            Metadata = metadata;
        }



    }
}
