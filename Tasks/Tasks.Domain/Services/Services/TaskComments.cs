using AutoMapper;
using Microsoft.Extensions.Localization;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Tasks;
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
        , IMapper _mapper) : ITaskComments
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

            return new ResponseModel<bool>
            {
                Success = true,
                Message = _localizer["CommentsAddedSuccessfully"]
            };
        }
    }
}
