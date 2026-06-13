namespace Tasks.Tasks.Domain.IUnitOfWork
{
    public interface ITasksModuleUoW
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
