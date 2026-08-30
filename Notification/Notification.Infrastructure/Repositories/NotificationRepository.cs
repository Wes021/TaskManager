using Notification.Notification.Domain.IRepositories;
using Notification.Notification.Domain.Models;
using Notification.Notification.Infrastructure.DbSettings;

namespace Notification.Notification.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationDbContext _context;

        public NotificationRepository(NotificationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Notifications model)
        {
            await _context.Notifications.AddAsync(model);
            return true;
        }
    }
}
