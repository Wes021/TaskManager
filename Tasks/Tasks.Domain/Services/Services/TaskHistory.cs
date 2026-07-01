using Microsoft.Extensions.Localization;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Tasks.TaskHistory;
using TaskManager.SharedLayer.ResponseModel;
using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Domain.IUnitOfWork;
using Tasks.Tasks.Domain.Services.IServices;

namespace Tasks.Tasks.Domain.Services.Services
{
    public class TaskHistory(IUserLookupService userLookupService, IStringLocalizer<SharedResource> _localizer,
       ITasksRepository _tasksRepository, ITasksModuleUoW _tasksModuleUoW,
       ITasksHistoryRepository _tasksHistoryRepository, ICurrentUserService _currentUserService) : ITaskHistory
    {
        public async Task<ResponseModel<bool>> AddNewHistory(int TaskId, AddTaskHistoryDTO model)
        {
            var task = await _tasksRepository.GetTaskById(TaskId);

            if (task is null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["TaskDoesNotExist"]
                };



            //        var isMember = task.Members.Any(x =>
            //x.UserId == _currentUserService.UserId &&
            //!x.IsDeleted &&
            //x.IsActive);


            //        if (!isMember)
            //        {
            //            return new ResponseModel<bool>
            //            {
            //                Success = false,
            //                Data = false,
            //                Message = _localizer["UserIsNotInTask"]
            //            };
            //        }

            var result = task.AddNewHistory(_currentUserService.UserId, model.actionDetails);

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
