using TaskManager.SharedLayer.ResponseModel;
using static TaskManager.SharedLayer.Enums.SystemEnums;

namespace Tasks.Tasks.Domain.Services.IServices
{
    public interface ITaskHistory
    {
        Task<ResponseModel<bool>> AddNewHistory(int TaskId, TaskHistoryAction action, string metaData);
    }
}
