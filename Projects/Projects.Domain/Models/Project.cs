using System.ComponentModel.DataAnnotations;
using TaskManager.SharedLayer.Enums;
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

        //public int ManagerId { get; private set; }

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

    int statusId,
    int createdUser)
        {
            if (string.IsNullOrWhiteSpace(name))
                return GenericDomainResponseModel<Project>.Fail("NameRequired");

            if (string.IsNullOrWhiteSpace(description))
                return GenericDomainResponseModel<Project>.Fail("DescriptionRequired");



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
                //ManagerId = managerId,
                StatusId = statusId,
                CreatedUser = createdUser,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            project.Members.Add(
                      new ProjectMember(
                         project.Id, createdUser, (int)SystemEnums.ProjectMemberRole.Leader, createdUser));
            return GenericDomainResponseModel<Project>.Success(project);
        }


        public GenericDomainResponseModel<Project> Update(
     string name,
     string description,
     DateTime startDate,
     DateTime? endDate,

     int statusId,
     int modifiedUser)
        {


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





        public GenericDomainResponseModel<List<int>> AddMembers(
      List<int> userIds,
      int assignedBy, int MemberRole)
        {
            var duplicateIds = userIds
                .Where(userId =>
                    Members.Any(x =>
                    x.ProjectId == Id &&
                        x.UserId == userId &&
                        !x.IsDeleted && x.IsActive))
                .Distinct()
                .ToList();

            if (duplicateIds.Any())
            {
                return new GenericDomainResponseModel<List<int>>
                {
                    Succeeded = false,
                    Error = "UsersAlreadyExist",
                    Data = duplicateIds
                };
            }

            foreach (var userId in userIds.Distinct())
            {
                Members.Add(
                    new ProjectMember(
                        Id,
                        userId,
                        MemberRole,
                        assignedBy));
            }

            return new GenericDomainResponseModel<List<int>>
            {
                Succeeded = true,
                Error = "UsersAddedSuccessfully"

            };
        }



        public DomainResponseModel RemoveMembers(
            List<int> userIds,
            int modifiedUser)
        {
            var membersToRemove = Members
                .Where(x =>
                    userIds.Contains(x.UserId) &&
                    !x.IsDeleted)
                .ToList();

            if (!membersToRemove.Any())
            {
                return DomainResponseModel
                    .Fail("MembersNotFound");
            }

            foreach (var member in membersToRemove)
            {
                member.Remove(modifiedUser);
            }

            return DomainResponseModel.Success();
        }







    }
}
