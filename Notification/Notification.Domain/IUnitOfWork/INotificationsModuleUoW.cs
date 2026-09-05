namespace Notification.Notification.Domain.IUnitOfWork
{
    public interface INotificationsModuleUoW
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
