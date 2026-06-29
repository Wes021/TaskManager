using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Tasks;

namespace Tasks.Tasks.Application.Handlers.IHandlers
{
    public interface ITasksHandler
    {
        Task<ResponseModel<bool>> AddNewTask(NewTaskRequestModel model);

        Task<ResponseModel<PagedResult<TaskInfoDto>>> GetTasksByCurrentUserId(GetTasksRequest model);

        Task<ResponseModel<PagedResult<TaskInfoDto>>> GetTasksByProjectID(GetTasksRequest model, int ProjectId);

        Task<ResponseModel<TaskInfoDetails>> GetTaskById(int TaskId);


        Task<ResponseModel<bool>> SetTaskStatus(int taskId, UpdateTaskStatus model);

        Task<ResponseModel<bool>> DeleteTask(int taskId, DeleteTask model);

        Task<ResponseModel<bool>> UpdateTask(int taskId, UpdateTaskInfo model);



        Task<ResponseModel<bool>> AddMembersToTask(AddMembersToCurrentTask model);

        Task<ResponseModel<bool>> RemoveMembersFromTask(RemoveMembersFromCurrentTask model);
    }
}
