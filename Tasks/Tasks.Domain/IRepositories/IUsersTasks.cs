using Tasks.Tasks.Domain.Models;

namespace Tasks.Tasks.Domain.IRepositories
{
    public interface IUsersTasks
    {
        Task<UsersTasks> Add(UsersTasks entity);
    }
}
