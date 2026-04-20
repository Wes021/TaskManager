using Identity.Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Module.Identity.Domain.IRepositories;

using Module.Identity.Infrastructure.DbSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
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
    }
}
