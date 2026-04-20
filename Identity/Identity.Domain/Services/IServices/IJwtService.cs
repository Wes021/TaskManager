using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Identity.Domain.Services.IServices
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string roles);
    }
}
