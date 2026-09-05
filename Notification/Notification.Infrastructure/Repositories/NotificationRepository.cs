using Microsoft.EntityFrameworkCore;
using Notification.Notification.Domain.IRepositories;
using Notification.Notification.Domain.Models;
using Notification.Notification.Infrastructure.DbSettings;
using TaskManager.SharedLayer.RequestModels.Notification;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Notifications;

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

        public Task<List<Notifications>> GetAllNotificationaByUserIdAsync(int UserId, Func<IQueryable<Notifications>, IQueryable<Notifications>>? include = null, bool isTracked = true)
        {
            IQueryable<Notifications> query = _context.Notifications.Where(x => x.UserId == UserId && !x.IsDeleted);

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            return query.ToListAsync();
        }

        public Task<Notifications> GetById(int Id, Func<IQueryable<Notifications>, IQueryable<Notifications>>? include = null, bool isTracked = true)
        {
            IQueryable<Notifications> query = _context.Notifications.Where(x => x.IsDeleted != true);

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);


            return query.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public Task<List<Notifications>> GetAllById(List<int> Ids, Func<IQueryable<Notifications>, IQueryable<Notifications>>? include = null, bool isTracked = true)
        {
            IQueryable<Notifications> query = _context.Notifications.Where(x => Ids.Contains(x.Id) && x.IsDeleted != true);

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);


            return query.ToListAsync();
        }

        public async Task<PagedResult<GetNotificationInfoDTO>> GetNotificationaByUserIdAsync(GetNotificationForUserDTO request, int UserId, Func<IQueryable<Notifications>, IQueryable<Notifications>>? include = null, bool isTracked = true)
        {
            IQueryable<Notifications> query = _context.Notifications.Where(x => x.UserId == UserId && !x.IsDeleted && x.IsRead == request.IsRead);

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            //Use Later For Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Title.Contains(request.Search));
            }


            if (request.IsActive.HasValue)
                query = query.Where(x => x.IsActive == request.IsActive.Value);


            if (request.IsDeleted.HasValue)
                query = query.Where(x => x.IsDeleted == request.IsDeleted.Value);


            //Sort by
            query = request.SortDir == "asc"
                ? query.OrderBy(x => x.CreatedDate)
                : query.OrderByDescending(x => x.CreatedDate);


            var totalCount = await query.CountAsync();





            // pagination
            var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new GetNotificationInfoDTO
            {
                Title = x.Title,
                Text = x.Text,
                TargetId = x.TargetId,
                IsRead = x.IsRead,
                Type = x.Type,
                CreatedDate = x.CreatedDate,



            })
            .ToListAsync();


            return new PagedResult<GetNotificationInfoDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
        }
    }
}
