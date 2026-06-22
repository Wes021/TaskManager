using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using TaskManager.SharedLayer.Enums;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using Tasks.Tasks.Application.Handlers.IHandlers;
using Tasks.Tasks.Domain.Services.IServices;

namespace Tasks.Tasks.Application.Handlers.Handlers
{
    public class TaskCommentsHandler(ICurrentUserService _currentUserService, IStringLocalizer<SharedResource> _localizer,
        IFileManager _fileManager, ITasksService _tasksService, ITaskComments _taskComments) : ITaskCommentsHandler
    {
        public async Task<ResponseModel<bool>> AddNewComment(int TaskId, AddNewComment model)
        {
            if (TaskId <= 0 || model.Text.IsNullOrEmpty())
                return new ResponseModel<bool>
                {
                    Success = false,

                    Message = _localizer["InvalidRequest"]
                };

            if (_currentUserService.Role !=
                SystemEnums.UserType.ManagerAndLeader.ToString() || _currentUserService.Role !=
                SystemEnums.UserType.Employee.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };

            var response = await _taskComments.AddNewComment(TaskId, model);

            return response;

        }

        public async Task<ResponseModel<bool>> DeleteComment(int commentId, int TaskId)
        {
            if (TaskId <= 0 || commentId <= 0)
                return new ResponseModel<bool>
                {
                    Success = false,

                    Message = _localizer["InvalidRequest"]
                };

            if (_currentUserService.Role !=
                SystemEnums.UserType.ManagerAndLeader.ToString() || _currentUserService.Role !=
                SystemEnums.UserType.Employee.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };

            var response = await _taskComments.DeleteComment(commentId, TaskId);

            return response;
        }
    }
}
