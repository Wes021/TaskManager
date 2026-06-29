using Tasks.Tasks.Domain.Models;

namespace Tasks.Tasks.Domain.IRepositories
{
    public interface ITasksHistoryRepository
    {
        Task<TaskHistory> Add(TaskHistory entity);
    }
}
