using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.ResponseModels;

namespace Projects.Projects.Domain.Models
{
    public class ProjectMember : IEntity, IAuditedEntity
    {
        [Key]

        public int Id { get; set; }
        public int ProjectId { get; private set; }
        public Project Project { get; set; }

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
            int assignedBy)
        {
            ProjectId = projectId;

            UserId = userId;

            AssignedBy = assignedBy;

            CreatedDate = DateTime.UtcNow;

            CreatedUser = assignedBy;

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
