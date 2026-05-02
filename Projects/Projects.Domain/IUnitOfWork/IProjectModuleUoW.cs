using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Projects.Domain.IUnitOfWork
{
    public interface IProjectModuleUoW
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
