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


        Task<ResponseModel<bool>> SetTaskStatus(int TaskId, UpdateTaskStatus model);


        Task<ResponseModel<bool>> DeleteTask(int TaskId, DeleteTask model);

        Task<ResponseModel<bool>> AddMembersToTask(AddMembersToCurrentTask model);

        Task<ResponseModel<bool>> RemoveMembersFromTask(RemoveMembersFromCurrentTask model);


        Task<ResponseModel<bool>> UpdateTask(int TaskId, UpdateTaskInfo model);




    }
}
