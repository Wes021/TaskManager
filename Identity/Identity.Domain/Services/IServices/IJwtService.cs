using Identity.Identity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.ResponseModels;

namespace Module.Identity.Domain.Services.IServices
{
    public interface IJwtService
    {
        string GenerateToken(JwtTokenData userModel);
    }
}
