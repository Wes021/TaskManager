using Identity.Identity.Domain.IUnitOfWork;
using Module.Identity.Infrastructure.DbSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Identity.Infrastructure.UnitOfWork
{
    public class IdentityMouduleUoW : IIdentityMouduleUoW
    {
        private readonly AppDbContext _context;

        public IdentityMouduleUoW(AppDbContext context)
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
