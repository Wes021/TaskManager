using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Identity.Domain.Services.IServices
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
