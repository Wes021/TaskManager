using TaskManager.SharedLayer.RequestModels.Tasks.TasksModels;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Tasks;

namespace Tasks.Tasks.Domain.IRepositories
{
    public interface ITasksRepository
    {
        Task<Tasks.Domain.Models.Tasks> Add(Tasks.Domain.Models.Tasks entity);



        Task<bool> ExistsByTitleAsync(NewTaskRequestModel entity);

        Task<bool> ExistsByTitleForUpdateAsync(int TaskId, UpdateTaskInfo entity);

        Task<PagedResult<TaskInfoDto>> GetTasksByUserIdAsync(GetTasksRequest request, int UserId, Func<IQueryable<Tasks.Domain.Models.Tasks>, IQueryable<Tasks.Domain.Models.Tasks>>? include = null, bool isTracked = true);


        Task<PagedResult<TaskInfoDto>> GetTasksByProjectIdAsync(GetTasksRequest request, int ProjectId, Func<IQueryable<Tasks.Domain.Models.Tasks>, IQueryable<Tasks.Domain.Models.Tasks>>? include = null, bool isTracked = true);

        Task<Domain.Models.Tasks> GetTaskById(int TaskId, Func<IQueryable<Tasks.Domain.Models.Tasks>, IQueryable<Tasks.Domain.Models.Tasks>>? include = null, bool isTracked = true);
    }
}
