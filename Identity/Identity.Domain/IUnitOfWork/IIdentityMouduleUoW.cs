using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Identity.Domain.IUnitOfWork
{
    public interface IIdentityMouduleUoW
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
