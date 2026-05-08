using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Identity;

namespace TaskManager.SharedLayer.Interfaces
{
    public interface IUserLookupService
    {
        Task<UserLookupDto?> GetUserByIdAsync(int userId);

        Task<List<UserLookupDto>> GetUsersByIdsAsync(List<int> userIds);
    }
}
