using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;

namespace Projects.Projects.Domain.Models
{
    public class ProjectMember : IEntity, IAuditedEntity
    {


        public int ProjectId { get; private set; }
        public Project Project { get; set; }

        public int UserId { get; private set; }

        public int AssignedBy { get; set; }
        public DateTime CreatedDate { get ; set ; }
        public int? CreatedUser { get ; set ; }
        public DateTime? ModifiedDate { get ; set ; }
        public int? ModifiedUser { get ; set ; }
        public int Id { get ; set ; }
         
        public bool IsDeleted { get ; set ; }
        public bool IsActive { get ; set ; }

        public ProjectMember(int projectId, int userId, int assignedBy)
        {
            ProjectId = projectId;
            UserId = userId;
            AssignedBy = assignedBy;
            CreatedDate = DateTime.Now;
            CreatedUser = assignedBy;
            IsDeleted = false;
            IsActive = true;
        }



        public void RemoveProjectMember(int ModifiedUserId)
        {
            ModifiedDate = DateTime.Now;
            ModifiedUser = ModifiedUserId;
      
            IsActive = false;
        }









    }
}
