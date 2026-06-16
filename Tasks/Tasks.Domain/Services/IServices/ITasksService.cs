using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Tasks;

namespace Tasks.Tasks.Domain.Services.IServices
{
    public interface ITasksService
    {
        Task<ResponseModel<bool>> AddNewTask(NewTaskRequestModel model, AddMembersToTask MembersModel, List<FileHandlerResponse> fileHandlerResponses);


        Task<ResponseModel<PagedResult<TaskInfoDto>>> GetTasksByUserId(GetTasksRequest model, int UserId);

        Task<ResponseModel<PagedResult<TaskInfoDto>>> GetTasksByProjectId(GetTasksRequest model, int ProjectId);


        Task<ResponseModel<TaskInfoDetails>> GetTaskById(int TaskId);


        Task<ResponseModel<bool>> SetTaskStatus(UpdateTaskStatus model);



    }
}
