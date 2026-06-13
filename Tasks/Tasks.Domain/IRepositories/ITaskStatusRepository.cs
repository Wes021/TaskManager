namespace Tasks.Tasks.Domain.IRepositories
{
    public interface ITaskStatusRepository
    {
        Task<bool> CheckTaskStatusExists(int StatusId);
    }
}
