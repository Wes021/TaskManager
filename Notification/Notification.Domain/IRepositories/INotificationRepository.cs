using Notification.Notification.Domain.Models;

namespace Notification.Notification.Domain.IRepositories
{
    public interface INotificationRepository
    {
        Task<bool> Add(Notifications model);
    }
}
