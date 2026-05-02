using Module.Projects.Infrastructure.DbSettings;
using Projects.Projects.Domain.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Projects.Infrastructure.UnitOfWork
{
    public class ProjectModuleUoW : IProjectModuleUoW
    {
        private readonly AppDbContext _context;

        public ProjectModuleUoW(AppDbContext context)
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
