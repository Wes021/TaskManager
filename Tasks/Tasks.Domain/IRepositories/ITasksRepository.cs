using TaskManager.SharedLayer.RequestModels.Tasks;

namespace Tasks.Tasks.Domain.IRepositories
{
    public interface ITasksRepository
    {
        Task<Tasks.Domain.Models.Tasks> Add(Tasks.Domain.Models.Tasks entity);


        Task<bool> ExistsByTitleAsync(AddNewTaksDTO entity);
    }
}
