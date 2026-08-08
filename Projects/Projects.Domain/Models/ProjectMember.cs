using System.ComponentModel.DataAnnotations;
using TaskManager.SharedLayer.Interfaces;

namespace Projects.Projects.Domain.Models
{
    public class ProjectMember : IEntity, IAuditedEntity
    {
        [Key]

        public int Id { get; set; }
        public int ProjectId { get; private set; }
        public Project Project { get; set; }

        public int ProjectMemberRoleId { get; set; }
        public ProjectMemberRole ProjectMemberRole { get; private set; }

        public int UserId { get; private set; }

        public int AssignedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }


        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }


        private ProjectMember() { }


        internal ProjectMember(
            int projectId,
            int userId,
            int projectMemberRoleId,
            int assignedBy)
        {
            ProjectId = projectId;

            UserId = userId;

            AssignedBy = assignedBy;

            CreatedDate = DateTime.UtcNow;

            CreatedUser = assignedBy;

            ProjectMemberRoleId = projectMemberRoleId;

            IsDeleted = false;

            IsActive = true;
        }


        internal void Remove(int modifiedUser)
        {
            IsDeleted = true;

            IsActive = false;

            ModifiedDate = DateTime.UtcNow;

            ModifiedUser = modifiedUser;
        }









    }
}
