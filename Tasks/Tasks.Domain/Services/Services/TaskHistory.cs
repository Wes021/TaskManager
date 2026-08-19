using Microsoft.Extensions.Localization;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.ResponseModel;
using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Domain.IUnitOfWork;
using Tasks.Tasks.Domain.Services.IServices;
using static TaskManager.SharedLayer.Enums.SystemEnums;

namespace Tasks.Tasks.Domain.Services.Services
{
    public class TaskHistory(IUserLookupService userLookupService, IStringLocalizer<SharedResource> _localizer,
       ITasksRepository _tasksRepository, ITasksModuleUoW _tasksModuleUoW,
       ITasksHistoryRepository _tasksHistoryRepository, ICurrentUserService _currentUserService) : ITaskHistory
    {
        public async Task<ResponseModel<bool>> AddNewHistory(int TaskId, TaskHistoryAction action, string metaData)
        {
            var task = await _tasksRepository.GetTaskById(TaskId);

            if (task is null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["TaskDoesNotExist"]
                };


            var result = task.AddNewHistory(TaskId, action, _currentUserService.UserId, metaData);

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

            return new ResponseModel<bool>
            {
                Success = true,
                Message = _localizer["HistoryAddedSuccessfully"]
            };


        }
    }
}
