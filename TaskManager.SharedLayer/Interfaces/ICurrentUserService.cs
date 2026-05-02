using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.Interfaces
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }

        int UserId { get; }

        string UserName { get; }

        string Role { get; }

        string? Email { get; }

        IEnumerable<string> Roles { get; }
    }
}
