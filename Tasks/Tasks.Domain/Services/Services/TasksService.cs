using AutoMapper;
using Microsoft.Extensions.Localization;
using TaskManager.SharedLayer.Enums;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Tasks.MembersModel;
using TaskManager.SharedLayer.RequestModels.Tasks.TaskHistory;
using TaskManager.SharedLayer.RequestModels.Tasks.TasksModels;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Tasks;
using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Domain.IUnitOfWork;
using Tasks.Tasks.Domain.Services.IServices;
using static TaskManager.SharedLayer.Enums.SystemEnums;

namespace Tasks.Tasks.Domain.Services.Services
{
    public class TasksService(IUserLookupService userLookupService, IStringLocalizer<SharedResource> _localizer,
        IProjectLookupService projectLookupService, ITasksRepository _tasksRepository,
        ICurrentUserService _currentUser, ITaskStatusRepository _taskStatusRepository,
        ITasksModuleUoW _tasksModuleUoW, IUsersTasks _usersTasks, IProjectLookupService _projectLookupService
        , IMapper _mapper, ITaskHistory _taskHistory
        ) : ITasksService


    {
        public async Task<ResponseModel<bool>> AddMembersToTask(AddMembersToCurrentTask model)
        {
            var task = await _tasksRepository.GetTaskById(model.TaskId);

            if (task is null)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["TaskNotFoundOrDeactivated"]
                };
            }


            var IsValidUsers = (await userLookupService.GetUsersByIdsAsync(model.MembersModels.MemberIds)).Where(x => x.IsDeleted || x.IsActive == false);

            if (IsValidUsers.Any())
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["OneOrMoreUsersAreNotValid"]
                };
            }


            var addMembersResult = task.AddMembersToTask(model.MembersModels.MemberIds, _currentUser.UserId);



            if (!addMembersResult.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = addMembersResult.Error
                };
            }


            model.MembersModels.MemberIds.ToString();
            await _tasksModuleUoW.SaveChangesAsync();

            await _taskHistory.AddNewHistory(
    task.Id,
    new AddTaskHistoryDTO
    {
        actionDetails = $"{SystemEnums.TaskHistoryActions.AddedNewMembers}"
    });

            return new ResponseModel<bool>
            {
                Success = true,
                Message = _localizer["MembersAddedSuccessfully"]
            };
        }



        public async Task<ResponseModel<bool>> AddNewTask(NewTaskRequestModel model, AddMembersToTask MembersModel, List<FileHandlerResponse> fileHandlerResponses)
        {
            var projectInfo = await projectLookupService.GetProjectById(model.ProjectId);


            if (projectInfo.Data == null || projectInfo.Data.IsDeleted == true || projectInfo.Data.IsActive == false)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["ProjectNotFoundOrDeactivated"]
                };

            if (await _tasksRepository.ExistsByTitleAsync(model))

                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "TitleAlreadyExists"
                };


            if (!await _taskStatusRepository.CheckTaskStatusExists(model.TaskStatus))
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "StatusDoesNotExists"
                };





            var newTaskResult = Tasks.Domain.Models.Tasks.Create(
    model.Title,
    model.Description,
    model.DueDate,
    model.TaskStatus,
    model.ProjectId,
    _currentUser.UserId
);

            if (!newTaskResult.Succeeded || newTaskResult.Data == null)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = newTaskResult.Error
                };
            }

            var newTask = newTaskResult.Data;

            // Now call AddMembersToTask on the instance, not the class
            var addMembersResult = newTask.AddMembersToTask(MembersModel.MemberIds, _currentUser.UserId);

            if (!addMembersResult.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = addMembersResult.Error
                };
            }


            var addAttachmentsResult = new GenericDomainResponseModel<List<int>>();

            if (fileHandlerResponses?.Any() == true)
            {
                addAttachmentsResult = newTask.AddAttachmentToTask(fileHandlerResponses, _currentUser.UserId);

                if (!addAttachmentsResult.Succeeded)
                {
                    return new ResponseModel<bool>
                    {
                        Success = false,
                        Data = false,
                        Message = addAttachmentsResult.Error
                    };
                }
            }





            await _tasksRepository.Add(newTask);

            await _tasksModuleUoW.SaveChangesAsync();

            await _taskHistory.AddNewHistory(newTask.Id, new AddTaskHistoryDTO { actionDetails = $"{SystemEnums.TaskHistoryActions.AddedNewMembers}" });

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["TaskAddedSuccessfully"]
            };
        }

        public async Task<ResponseModel<bool>> DeleteTask(int TaskId, DeleteTask model)
        {
            var task = await _tasksRepository.GetTaskById(TaskId);
            var CurrectUser = _currentUser.UserId;



            if (task is null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["TaskDoesNotExist"]
                };

            var Taskresult = task.SetIsDeleted(model.IsDeleted, CurrectUser);
            var TaskMembersResult = task.RemoveMembers(task.Members.Where(x => x.TasksId == task.Id).Select(x => x.UserId).ToList(), CurrectUser);
            var TaskAttachemnetResult = task.RemoveAttachments(task.TaskAttachments.Where(x => x.TasksId == task.Id).Select(x => x.Id).ToList(), CurrectUser);

            if (!Taskresult.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer[Taskresult.Error]
                };

            if (!TaskMembersResult.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer[TaskMembersResult.Error]
                };

            if (!TaskAttachemnetResult.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer[TaskAttachemnetResult.Error]
                };

            await _tasksModuleUoW.SaveChangesAsync();
            await _taskHistory.AddNewHistory(task.Id, new AddTaskHistoryDTO { actionDetails = $"{SystemEnums.TaskHistoryActions.DeletedTheTask}" });
            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["TaskDeletedSuccessfully"]
            };
        }

        public async Task<ResponseModel<TaskInfoDetails>> GetTaskById(int TaskId)
        {
            var TaskDetails = await _tasksRepository.GetTaskById(TaskId);




            if (TaskDetails == null)
            {
                return new ResponseModel<TaskInfoDetails>
                {
                    Success = true,

                    Message = _localizer["NoTaskWasFound"]
                };
            }



            if (!(TaskDetails.Members.Any(m => m.IsDeleted == false && m.UserId == _currentUser.UserId)))
            {
                return new ResponseModel<TaskInfoDetails>
                {
                    Success = true,

                    Message = _localizer["NoTaskWasFound"]
                };
            }



            var projects = await _projectLookupService
    .GetProjectById(TaskDetails.ProjectId);

            var MappedData = _mapper.Map<TaskInfoDetails>(TaskDetails);
            MappedData.ProjectName = projects.Data.Name;
            MappedData.TaskMembers = _mapper.Map<List<TaskMembersDto>>(await userLookupService.GetUsersByIdsAsync(TaskDetails.Members.Select(x => x.UserId).ToList()));
            MappedData.TaskComments = _mapper.Map<List<TaskCommentsDto>>(TaskDetails.TaskComments);
            var userIds = TaskDetails.TaskHistory
    .Where(x => x.CreatedUser.HasValue)
    .Select(x => x.CreatedUser.Value)
    .Distinct()
    .ToList();


            var users = await userLookupService.GetUsersByIdsAsync(userIds);


            var usersDictionary = users
                .ToDictionary(x => x.Id, x => x.FullName);


            MappedData.TaskHistory = TaskDetails.TaskHistory
                .Select(x => new TaskHistoryDTO
                {
                    FullName = x.CreatedUser.HasValue &&
                               usersDictionary.ContainsKey(x.CreatedUser.Value)
                        ? usersDictionary[x.CreatedUser.Value]
                        : null,

                    ActionDetails = x.ActionDetails
                })
                .ToList();



            return new ResponseModel<TaskInfoDetails>
            {
                Success = true,
                Data = MappedData,
                Message = _localizer["DataRetunedSuccssefully"]
            };
        }

        public async Task<ResponseModel<PagedResult<TaskInfoDto>>> GetTasksByProjectId(GetTasksRequest model, int ProjectId)
        {
            var tasksResult = await _tasksRepository.GetTasksByProjectIdAsync(model, ProjectId);

            var projectIds = tasksResult.Items.Select(x => x.ProjectId).Distinct().ToList();

            var projects = await _projectLookupService
    .GetProjectsByIds(projectIds);

            var projectLookup = projects.Data.ToDictionary(x => x.Id, x => x.Name);

            foreach (var task in tasksResult.Items)
            {
                task.ProjectName =
                    projectLookup.GetValueOrDefault(task.ProjectId);
            }



            return new ResponseModel<PagedResult<TaskInfoDto>>
            {
                Success = true,
                Data = tasksResult,
                Message = tasksResult != null
        ? _localizer["DataRetunedSuccssefully"]
        : _localizer["NoDataFound"]
            };
        }



        public async Task<ResponseModel<PagedResult<TaskInfoDto>>> GetTasksByUserId(GetTasksRequest model, int UserId)
        {
            var tasksResult = await _tasksRepository.GetTasksByUserIdAsync(model, UserId);

            var projectIds = tasksResult.Items.Select(x => x.ProjectId).Distinct().ToList();

            var projects = await _projectLookupService
    .GetProjectsByIds(projectIds);

            var projectLookup = projects.Data.ToDictionary(x => x.Id, x => x.Name);

            foreach (var task in tasksResult.Items)
            {
                task.ProjectName =
                    projectLookup.GetValueOrDefault(task.ProjectId);
            }



            return new ResponseModel<PagedResult<TaskInfoDto>>
            {
                Success = true,
                Data = tasksResult,
                Message = tasksResult != null
        ? _localizer["DataRetunedSuccssefully"]
        : _localizer["NoDataFound"]
            };

        }

        public async Task<ResponseModel<bool>> SetTaskStatus(int TaskId, UpdateTaskStatus model)
        {
            var task = await _tasksRepository.GetTaskById(TaskId);
            var CurrectUser = _currentUser.UserId;
            var StatusExists = await _taskStatusRepository.CheckTaskStatusExists(model.NewStatus);

            if (StatusExists == false)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["TaskStatusDoesNotExist"]
                };

            if (task is null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["TaskDoesNotExist"]
                };


            var status = (TaskStatuseEnums)model.NewStatus;

            var result = task.SetNewStatus(status, CurrectUser);


            if (!result.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer[result.Error]
                };

            await _tasksModuleUoW.SaveChangesAsync();
            await _taskHistory.AddNewHistory(task.Id, new AddTaskHistoryDTO { actionDetails = $"{SystemEnums.TaskHistoryActions.UpdatedTheStatus}" });
            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["TaskStatusUpdatedSuccessfully"]
            };
        }

        public async Task<ResponseModel<bool>> RemoveMembersFromTask(RemoveMembersFromCurrentTask model)
        {
            var task = await _tasksRepository.GetTaskById(model.TaskId);

            if (task is null)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["TaskNotFoundOrDeactivated"]
                };
            }


            var IsValidUsers = (await userLookupService.GetUsersByIdsAsync(model.MembersModels.MemberIds)).Where(x => x.IsDeleted || x.IsActive == false);

            if (IsValidUsers.Any())
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["OneOrMoreUsersAreNotValid"]
                };
            }


            var addMembersResult = task.RemoveMembers(model.MembersModels.MemberIds, _currentUser.UserId);



            if (!addMembersResult.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = addMembersResult.Error
                };
            }



            await _tasksModuleUoW.SaveChangesAsync();
            await _taskHistory.AddNewHistory(task.Id, new AddTaskHistoryDTO { actionDetails = $"{SystemEnums.TaskHistoryActions.RemovedMember}" });
            return new ResponseModel<bool>
            {
                Success = true,
                Message = _localizer["MembersRemovedSuccessfully"]
            };
        }

        public async Task<ResponseModel<bool>> UpdateTask(int TaskId, UpdateTaskInfo model)
        {

            var task = await _tasksRepository.GetTaskById(TaskId, null, true);

            if (task is null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["TaskDoesNotExist"]
                };



            if (await _tasksRepository.ExistsByTitleForUpdateAsync(TaskId, model))

                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "TitleAlreadyExists"
                };

            var result = task.Update(model.Title, model.Description, model.DueDate, _currentUser.UserId);



            if (!result.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer[result.Error]
                };

            await _tasksModuleUoW.SaveChangesAsync();
            await _taskHistory.AddNewHistory(task.Id, new AddTaskHistoryDTO { actionDetails = $"{SystemEnums.TaskHistoryActions.UpdatedTheTask}" });

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["TaskUpdatedSuccessfully"]
            };

        }
    }
}
