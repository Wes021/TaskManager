using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;

namespace Tasks.Tasks.Application.Handlers.IHandlers
{
    public interface ITasksHandler
    {
        Task<ResponseModel<bool>> AddNewTask(NewTaskRequestModel model);
    }
}
