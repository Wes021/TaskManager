using Microsoft.Extensions.Localization;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.TaskHistory;
using TaskManager.SharedLayer.ResponseModel;
using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Domain.IUnitOfWork;
using Tasks.Tasks.Domain.Services.IServices;

namespace Tasks.Tasks.Domain.Services.Services
{
    public class TaskHistory(IUserLookupService userLookupService, IStringLocalizer<SharedResource> _localizer,
       ITasksRepository _tasksRepository, ITasksModuleUoW _tasksModuleUoW) : ITaskHistory
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


            throw new NotImplementedException();
        }
    }
}
