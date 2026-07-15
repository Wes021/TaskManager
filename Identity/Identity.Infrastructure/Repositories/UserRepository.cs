using Identity.Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Module.Identity.Domain.IRepositories;

using Module.Identity.Infrastructure.DbSettings;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModels;

namespace Module.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _context;

        public UserRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User entity)
        {
            _context.Users.Add(entity);
            return entity;
        }

        public async Task<bool> CheckUserExistsAsync(AddNewUserDTO request)
        {
            return await _context.Users
        .AsNoTracking()
        .AnyAsync(x => x.UserName == request.UserName || x.Email == request.Email || x.phonenumber == request.phonenumber);
        }

        public async Task<User?> GetByEmail(string email, Func<IQueryable<User>, IQueryable<User>>? include = null, bool isTracked = true)
        {
            IQueryable<User> query = _context.Users;

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetById(int Id, Func<IQueryable<User>, IQueryable<User>>? include = null, bool isTracked = true)
        {
            IQueryable<User> query = _context.Users;

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<User?> GetByUsername(
    string username,
    Func<IQueryable<User>, IQueryable<User>>? include = null,
    bool isTracked = true)
        {
            IQueryable<User> query = _context.Users;

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<PagedResult<UserInfoDTO>> GetUsersAsync(GetUsersRequest request, Func<IQueryable<User>, IQueryable<User>>? include = null, bool isTracked = true)
        {
            IQueryable<User> query = _context.Users;

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);


            //Use Later For Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.FullName.Contains(request.Search) ||
                x.Email.Contains(request.Search)
                );
            }

            //Filter by Role
            if (!string.IsNullOrWhiteSpace(request.Role))
                query = query.Where(x => x.RoleId == request.RoleId);

            //Filter by active or inactive
            if (request.IsActive.HasValue)
                query = query.Where(x => x.IsActive == request.IsActive.Value);


            //Sort by
            query = request.SortDir == "asc"
                ? query.OrderBy(x => x.CreatedDate)
                : query.OrderByDescending(x => x.CreatedDate);


            var totalCount = await query.CountAsync();


            // pagination
            var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new UserInfoDTO
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                UserName = x.UserName,
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate,
                Role = x.Role.Name

            })
            .ToListAsync();


            return new PagedResult<UserInfoDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };

        }



        public async Task<List<UserLookupDto>> GetUsersByIdsAsync(
    List<int> userIds)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(x => userIds.Contains(x.Id))
                .Select(x => new UserLookupDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    UserName = x.UserName,
                    Email = x.Email,
                    phonenumber = x.phonenumber,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted
                })
                .ToListAsync();
        }
    }
}
