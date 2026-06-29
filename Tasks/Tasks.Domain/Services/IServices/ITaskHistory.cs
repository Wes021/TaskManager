using TaskManager.SharedLayer.RequestModels.TaskHistory;
using TaskManager.SharedLayer.ResponseModel;

namespace Tasks.Tasks.Domain.Services.IServices
{
    public interface ITaskHistory
    {
        Task<ResponseModel<bool>> AddNewHistory(int TaskId, AddTaskHistoryDTO model);
    }
}
