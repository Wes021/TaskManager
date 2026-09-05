using Notification.Notification.Domain.IUnitOfWork;
using Notification.Notification.Infrastructure.DbSettings;

namespace Notification.Notification.Infrastructure.UnitOfWork
{
    public class NotificationsModuleUoW : INotificationsModuleUoW
    {
        private readonly NotificationDbContext _context;

        public NotificationsModuleUoW(NotificationDbContext context)
        {
            _context = context;
        }
        public Task<int> SaveChangesAsync(
       CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
