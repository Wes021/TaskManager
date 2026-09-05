using Notification.Notification.Domain.Models;
using TaskManager.SharedLayer.RequestModels.Notification;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Notifications;

namespace Notification.Notification.Domain.IRepositories
{
    public interface INotificationRepository
    {
        Task<bool> Add(Notifications model);

        Task<PagedResult<GetNotificationInfoDTO>> GetNotificationaByUserIdAsync(GetNotificationForUserDTO request, int UserId, Func<IQueryable<Notifications>, IQueryable<Notifications>>? include = null, bool isTracked = true);

        Task<List<Notifications>> GetAllNotificationaByUserIdAsync(int UserId, Func<IQueryable<Notifications>, IQueryable<Notifications>>? include = null, bool isTracked = true);

        Task<Notifications> GetById(int Id, Func<IQueryable<Notifications>, IQueryable<Notifications>>? include = null, bool isTracked = true);

        Task<List<Notifications>> GetAllById(List<int> Ids, Func<IQueryable<Notifications>, IQueryable<Notifications>>? include = null, bool isTracked = true);
    }
}
