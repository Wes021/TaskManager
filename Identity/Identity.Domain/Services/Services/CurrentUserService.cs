using Identity.Identity.Domain.Services.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;

namespace Identity.Identity.Domain.Services.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal User =>
            _httpContextAccessor.HttpContext?.User;

        public bool IsAuthenticated =>
            User?.Identity?.IsAuthenticated ?? false;

        public int UserId =>
        int.TryParse(
            User?.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            out var id)
            ? id : 0;

        public string UserName =>
            User.FindFirst(ClaimTypes.Name)?.Value ?? "";

        public string Role =>
            User.FindFirst(ClaimTypes.Role)?.Value ?? "";

        public string? Email =>
            User.FindFirst(ClaimTypes.Email)?.Value;

        public IEnumerable<string> Roles =>
            User.FindAll(ClaimTypes.Role).Select(x => x.Value);
    }
}

