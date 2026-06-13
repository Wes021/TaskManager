using System.ComponentModel.DataAnnotations;
using System.Data;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Tasks;

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
                Error = "AttachemnetsAddedSuccseefully"

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
    }
}
