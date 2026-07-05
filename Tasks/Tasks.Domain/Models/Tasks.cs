using System.ComponentModel.DataAnnotations;
using System.Data;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Tasks;
using static TaskManager.SharedLayer.Enums.SystemEnums;

namespace Tasks.Tasks.Domain.Models
{
    public class Tasks : IEntity, IAuditedEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }



        public DateTime DueDate { get; set; }

        public int TasksStatusId { get; set; }
        public TasksStatus TasksStatus { get; set; }


        public int ProjectId { get; set; }




        public List<TaskAttachments> TaskAttachments { get; private set; } = [];

        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }


        public List<UsersTasks> Members { get; private set; } = [];


        public List<TaskComments> TaskComments { get; private set; } = [];
        public List<TaskHistory> TaskHistory { get; private set; } = [];



        public GenericDomainResponseModel<bool> AddNewHistory(int createdUser, string actionDetails)
        {
            if (string.IsNullOrWhiteSpace(actionDetails))
                return GenericDomainResponseModel<bool>.Fail("ActionRequired");

            if (createdUser <= 0)
                return GenericDomainResponseModel<bool>.Fail("UserIdInvalid");

            TaskHistory.Add(new TaskHistory(createdUser, actionDetails));


            return new GenericDomainResponseModel<bool>
            {
                Succeeded = true

            };

        }


        public GenericDomainResponseModel<Tasks> Update(
      string title,
      string description,
      DateTime dueDate,
      int? modifiedUser)
        {
            if (string.IsNullOrWhiteSpace(title))
                return GenericDomainResponseModel<Tasks>.Fail("TitleRequired");

            if (string.IsNullOrWhiteSpace(description))
                return GenericDomainResponseModel<Tasks>.Fail("DescriptionRequired");

            Title = title;
            Description = description;
            DueDate = dueDate;

            ModifiedDate = DateTime.UtcNow;
            ModifiedUser = modifiedUser;

            return GenericDomainResponseModel<Tasks>.Success(this);
        }


        public GenericDomainResponseModel<List<int>> AddMembersToTask(
      List<int> userIds,
      int assignedBy)
        {
            var existingUserIds = Members.Where(x => x.IsDeleted != true && x.IsActive != false).Select(x => x.UserId).ToHashSet();

            var dublicatedIds = userIds.Where(userId => existingUserIds.Contains(userId))
                .Distinct()
                .ToList();


            if (dublicatedIds.Any())
            {
                return new GenericDomainResponseModel<List<int>>
                {
                    Succeeded = false,
                    Error = "UsersAlreadyExist",
                    Data = dublicatedIds
                };
            }

            foreach (var userId in userIds.Distinct())
            {
                Members.Add(new UsersTasks(Id, userId, assignedBy));
            }

            return new GenericDomainResponseModel<List<int>>
            {
                Succeeded = true,
                Error = "UsersAddedSuccessfully"

            };

        }



        public GenericDomainResponseModel<bool> AddCommentsToTask(int taskId, string text, int createdUser)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new GenericDomainResponseModel<bool>
                {
                    Succeeded = false,
                    Error = "CommentTextRequired"
                };
            }



            TaskComments.Add(new TaskComments(taskId, text, createdUser));


            return new GenericDomainResponseModel<bool>
            {
                Succeeded = true

            };
        }

        public GenericDomainResponseModel<bool> RemoveCommentsToTask(int commentId, int modifiedUser)
        {

            var commentToRemove = TaskComments
                .Where(c => !c.IsDeleted && c.IsActive && c.CreatedUser == modifiedUser && c.Id == commentId).FirstOrDefault();



            if (commentToRemove is null || commentToRemove.IsDeleted == true)
            {
                return new GenericDomainResponseModel<bool>
                {
                    Succeeded = false,
                    Error = "CommentDoesNotExists"
                };
            }



            commentToRemove.RemoveComment(modifiedUser);


            return new GenericDomainResponseModel<bool>
            {
                Succeeded = true,
                Error = "CommentRemovedSuccessfully"
            };
        }





        public GenericDomainResponseModel<List<int>> AddAttachmentToTask(
      List<FileHandlerResponse> attachmentDTOs,
      int addedBy)
        {

            foreach (var atttachement in attachmentDTOs)
            {
                TaskAttachments.Add(new TaskAttachments(Id, atttachement.FileType, atttachement.FileName, atttachement.FilePath, atttachement.FileSize, addedBy));
            }


            return new GenericDomainResponseModel<List<int>>
            {
                Succeeded = true,


            };
        }



        public static GenericDomainResponseModel<Tasks> Create(string title, string description,
            DateTime dueDate, int tasksStatusId,
            int projectId, int? createdUser
            )
        {

            if (string.IsNullOrWhiteSpace(title))
                return GenericDomainResponseModel<Tasks>.Fail("TitleRequired");

            if (string.IsNullOrWhiteSpace(description))
                return GenericDomainResponseModel<Tasks>.Fail("DescriptionRequired");

            if (projectId <= 0)
                return GenericDomainResponseModel<Tasks>.Fail("InvalidProject");

            if (tasksStatusId <= 0)
                return GenericDomainResponseModel<Tasks>.Fail("InvalidStatus");

            if (createdUser <= 0)
                return GenericDomainResponseModel<Tasks>.Fail("InvalidUser");


            var task = new Tasks
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                TasksStatusId = tasksStatusId,
                ProjectId = projectId,
                CreatedUser = createdUser,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                IsActive = true

            };

            return GenericDomainResponseModel<Tasks>.Success(task);

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






        public static class TaskStatusTransitions
        {
            public static readonly Dictionary<TaskStatuseEnums, TaskStatuseEnums[]> Allowed =
                new()
                {
            { TaskStatuseEnums.Draft,     [TaskStatuseEnums.Active, TaskStatuseEnums.Cancelled] },
            { TaskStatuseEnums.Active,    [TaskStatuseEnums.Completed, TaskStatuseEnums.Cancelled] },
            { TaskStatuseEnums.Completed, [TaskStatuseEnums.Reopen] },
            { TaskStatuseEnums.Cancelled, [TaskStatuseEnums.Active] },
            { TaskStatuseEnums.Reopen, [TaskStatuseEnums.Completed] }
                };
        }




        public DomainResponseModel SetNewStatus(
    TaskStatuseEnums newStatus,
    int modifiedUser)
        {
            var currentStatus = (TaskStatuseEnums)TasksStatusId;

            if (currentStatus == newStatus)
                return DomainResponseModel.Fail("NoChangesDetected");

            if (!TaskStatusTransitions.Allowed.TryGetValue(
                    currentStatus,
                    out var allowedStatuses))
            {
                return DomainResponseModel.Fail("InvalidStatus");
            }

            if (!allowedStatuses.Contains(newStatus))
                return DomainResponseModel.Fail("InvalidStatusTransition");

            TasksStatusId = (int)newStatus;

            ModifiedDate = DateTime.Now;
            ModifiedUser = modifiedUser;

            return DomainResponseModel.Success();
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
                member.DeleteOrRemove(modifiedUser);
            }

            return DomainResponseModel.Success();
        }




        public DomainResponseModel RemoveAttachments(
            List<int> attachementIds,
            int modifiedUser)
        {
            var attachementsToRemove = TaskAttachments
                .Where(x =>
                    attachementIds.Contains(x.Id) &&
                    !x.IsDeleted)
                .ToList();

            if (!attachementsToRemove.Any())
            {
                return DomainResponseModel.Success();
            }

            foreach (var attachements in attachementsToRemove)
            {
                attachements.Remove(modifiedUser);
            }

            return DomainResponseModel.Success();
        }










    }
}
