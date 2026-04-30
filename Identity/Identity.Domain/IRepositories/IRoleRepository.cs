using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels;

namespace Identity.Identity.Domain.IRepositories
{
    public interface IRoleRepository
    {
        Task<bool> CheckRoleExistsAsync(int RoleId);

    }
}
