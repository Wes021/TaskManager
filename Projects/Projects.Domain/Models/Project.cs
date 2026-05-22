using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.ResponseModels;

namespace Projects.Projects.Domain.Models
{
    public class Project : IEntity, IAuditedEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public int ManagerId { get; private set; }

        public ProjectStatus Status { get; private set; }
        public int StatusId { get; set; }

        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public List<ProjectMember> Members { get; private set; } = [];



        public static GenericDomainResponseModel<Project> Create(
    string name,
    string description,
    DateTime startDate,
    DateTime? endDate,
    int managerId,
    int statusId,
    int createdUser)
        {
            if (string.IsNullOrWhiteSpace(name))
                return GenericDomainResponseModel<Project>.Fail("NameRequired");

            if (string.IsNullOrWhiteSpace(description))
                return GenericDomainResponseModel<Project>.Fail("DescriptionRequired");

            if (managerId <= 0)
                return GenericDomainResponseModel<Project>.Fail("InvalidManager");

            if (statusId <= 0)
                return GenericDomainResponseModel<Project>.Fail("InvalidStatus");

            if (createdUser <= 0)
                return GenericDomainResponseModel<Project>.Fail("InvalidUser");

           

            if (endDate.HasValue && endDate.Value <= startDate)
                return GenericDomainResponseModel<Project>.Fail("EndDateBeforeStartDate");

            var project = new Project
            {
                Name = name.Trim(),
                Description = description.Trim(),
                StartDate = startDate,
                EndDate = endDate,
                ManagerId = managerId,
                StatusId = statusId,
                CreatedUser = createdUser,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            return GenericDomainResponseModel<Project>.Success(project);
        }


        public GenericDomainResponseModel<Project> Update(
     string name,
     string description,
     DateTime startDate,
     DateTime? endDate,
     int managerId,
     int statusId,
     int modifiedUser)
        {
            if (managerId <= 0)
                return GenericDomainResponseModel<Project>
                    .Fail("InvalidManager");

            if (statusId <= 0)
                return GenericDomainResponseModel<Project>
                    .Fail("InvalidStatus");

            if (modifiedUser <= 0)
                return GenericDomainResponseModel<Project>
                    .Fail("InvalidUser");

            if (endDate.HasValue && endDate.Value <= startDate)
                return GenericDomainResponseModel<Project>
                    .Fail("EndDateBeforeStartDate");

            Name = name.Trim();

            Description = description.Trim();

            StartDate = startDate;

            EndDate = endDate;

            ManagerId = managerId;

            StatusId = statusId;

            ModifiedDate = DateTime.UtcNow;

            ModifiedUser = modifiedUser;

            return GenericDomainResponseModel<Project>
                .Success(this);
        }


        public DomainResponseModel SetIsActive(bool isActive, int modifiedUser)
        {
            if (IsActive == isActive)
                return DomainResponseModel.Fail("NoChangesDetected");

            

            if (IsDeleted)
                return DomainResponseModel.Fail("DeletedProjectStatusBlocked");

            IsActive = isActive;
            ModifiedDate = DateTime.UtcNow;
            ModifiedUser = modifiedUser;

            return DomainResponseModel.Success();
        }

        public DomainResponseModel SetIsDeleted(bool isDeleted, int modifiedUser)
        {
            if (IsDeleted == isDeleted)
                return DomainResponseModel.Fail("NoChangesDetected");

            if (!isDeleted)
                return DomainResponseModel.Fail("CantRestoreProject");

      

            IsDeleted = isDeleted;
            IsActive = false;
            ModifiedDate = DateTime.UtcNow;
            ModifiedUser = modifiedUser;

            return DomainResponseModel.Success();
        }



        public DomainResponseModel AddMembers(List<int> userIds, int assignedBy)
        {
            foreach (var userId in userIds.Distinct())
            {
                if (Members.Any(x => x.UserId == userId))
                    continue;

                Members.Add(new ProjectMember(
                    Id,
                    userId,
                    assignedBy));

               
            }
            return DomainResponseModel.Success();
        }


        public DomainResponseModel RemoveMembers(List<int> userIds, int modifiedUser)
        {
            var membersToRemove = Members.Where(x => userIds.Contains(x.UserId) && x.IsActive).ToList();


            if (!membersToRemove.Any())
            {
                DomainResponseModel.Fail("Users not found");
            }

            foreach (var member in membersToRemove)
            {
                member.RemoveProjectMember(modifiedUser);
            }
            return DomainResponseModel.Success();
        }
    }
}
