using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels.Tasks;

namespace Tasks.Tasks.Domain.Services.IServices
{
    public interface ITasksService
    {
        Task<ResponseModel<bool>> AddNewTask(NewTaskRequestModel model, AddMembersToTask MembersModel, List<FileHandlerResponse> fileHandlerResponses);
    }
}
