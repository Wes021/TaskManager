using Identity.Identity.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Module.Identity.Infrastructure.DbSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Identity.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckRoleExistsAsync(int RoleId)
        {
            return await _context.Roles.AsNoTracking().AnyAsync(x => x.Id == RoleId && x.IsDeleted != true && x.IsActive != false);
            throw new NotImplementedException();
        }
    }
}
