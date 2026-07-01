using AutoMapper;
using Microsoft.Extensions.Localization;
using TaskManager.SharedLayer.Enums;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Tasks.CommentsModel;
using TaskManager.SharedLayer.RequestModels.Tasks.TaskHistory;
using TaskManager.SharedLayer.ResponseModel;
using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Domain.IUnitOfWork;
using Tasks.Tasks.Domain.Services.IServices;

namespace Tasks.Tasks.Domain.Services.Services
{
    public class TaskComments(IUserLookupService userLookupService, IStringLocalizer<SharedResource> _localizer,
        IProjectLookupService projectLookupService, ITasksRepository _tasksRepository,
        ICurrentUserService _currentUser, ITaskStatusRepository _taskStatusRepository,
        ITasksModuleUoW _tasksModuleUoW, IUsersTasks _usersTasks, IProjectLookupService _projectLookupService
        , IMapper _mapper, ITaskHistory _taskHistory) : ITaskComments
    {
        public async Task<ResponseModel<bool>> AddNewComment(int TaskId, AddNewComment model)
        {
            var task = await _tasksRepository.GetTaskById(TaskId);

            if (task is null)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["TaskNotFoundOrDeactivated"]
                };
            }


            if (!task.Members.Select(x => x.UserId).Contains(_currentUser.UserId))
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["YouAreNotaValidMemberOfThisTask"]
                };
            }

            var addCommentsToTask = task.AddCommentsToTask(TaskId, model.Text, _currentUser.UserId);

            if (!addCommentsToTask.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = addCommentsToTask.Error
                };
            }



            await _tasksModuleUoW.SaveChangesAsync();
            await _taskHistory.AddNewHistory(task.Id, new AddTaskHistoryDTO { actionDetails = $"{SystemEnums.TaskHistoryActions.AddedNewComment}" });
            return new ResponseModel<bool>
            {
                Success = true,
                Message = _localizer["CommentsAddedSuccessfully"]
            };
        }

        public async Task<ResponseModel<bool>> DeleteComment(int commentId, int TaskId)
        {
            var task = await _tasksRepository.GetTaskById(TaskId);
            var comments = task.TaskComments;
            if (task is null)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["TaskNotFoundOrDeactivated"]
                };
            }

            if (!task.Members.Select(x => x.UserId).Contains(_currentUser.UserId))
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["YouAreNotaValidMemberOfThisTask"]
                };
            }

            if (!comments.Select(x => x.CreatedUser).Contains(_currentUser.UserId))
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["CantRemoveCommentAddedBySomeoneElse"]
                };
            }


            var result = task.RemoveCommentsToTask(commentId, _currentUser.UserId);




            if (!result.Succeeded)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = result.Error
                };
            }



            await _tasksModuleUoW.SaveChangesAsync();
            await _taskHistory.AddNewHistory(task.Id, new AddTaskHistoryDTO { actionDetails = $"{SystemEnums.TaskHistoryActions.DeletedAComment}" });
            return new ResponseModel<bool>
            {
                Success = true,
                Message = _localizer["CommentRemovedSuccessfully"]
            };
        }
    }
}
