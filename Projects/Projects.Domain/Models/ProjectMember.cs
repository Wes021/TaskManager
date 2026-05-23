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

        //public ProjectMember(int projectId, int userId, int assignedBy)
        //{
        //    ProjectId = projectId;
        //    UserId = userId;
        //    AssignedBy = assignedBy;
        //    CreatedDate = DateTime.Now;
        //    CreatedUser = assignedBy;
        //    IsDeleted = false;
        //    IsActive = true;
        //}



        //public void RemoveProjectMember(int ModifiedUserId)
        //{
        //    ModifiedDate = DateTime.Now;
        //    ModifiedUser = ModifiedUserId;

        //    IsActive = false;
        //}


        public static GenericDomainResponseModel<List<ProjectMember>> AddMembers(
         int projectId,
         List<int> userIds,
         int assignedBy)
        {
            var members = new List<ProjectMember>();

            foreach (var userId in userIds.Distinct())
            {
                var projectMember = new ProjectMember
                {
                    ProjectId = projectId,
                    UserId = userId,

                    CreatedDate = DateTime.UtcNow,
                    CreatedUser = assignedBy,

                    IsActive = true,
                    IsDeleted = false
                };

                members.Add(projectMember);
            }

            return GenericDomainResponseModel<List<ProjectMember>>
                .Success(members);
        }


        public DomainResponseModel RemoveMembers(List<int> userIds, int modifiedUser)
        {


            foreach (var member in userIds)
            {
                ModifiedDate = DateTime.Now;
                ModifiedUser = modifiedUser;

                IsActive = false;
            }
            return DomainResponseModel.Success();
        }









    }
}
