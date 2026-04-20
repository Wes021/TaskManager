using Identity.Identity.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Identity.Domain.IRepositories
{
    public interface IUserRepository
    {
         Task<User?> GetByUsername(
   string username,
   Func<IQueryable<User>, IQueryable<User>>? include = null,
   bool isTracked = true);
    }
}
