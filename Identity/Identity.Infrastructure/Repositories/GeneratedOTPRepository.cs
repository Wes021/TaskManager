using Identity.Identity.Domain.IRepositories;
using Identity.Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Module.Identity.Infrastructure.DbSettings;
using TaskManager.SharedLayer.RequestModels.Identity;

namespace Identity.Identity.Infrastructure.Repositories
{
    public class GeneratedOTPRepository : IGeneratedOTPRepository
    {
        private readonly IdentityDbContext _context;
        private readonly IConfiguration _configuration;
        public GeneratedOTPRepository(IdentityDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<GeneratedOTP> Add(GeneratedOTP entity)
        {
            _context.Add(entity);
            return entity;
        }

        public async Task<GeneratedOTP?> GetGeneratedOTPAsync(GetOtpSearchDto model)
        {
            IQueryable<GeneratedOTP> query = _context.GeneratedOTP;


            return await query.FirstOrDefaultAsync(x => x.UserId == model.UserId && x.IsActive && !x.IsDeleted && x.Attempts < _configuration.GetValue<int>("OtpSettings:MaxAlowedAttempts") && x.ExpiresAt <= DateTime.Now.AddSeconds(_configuration.GetValue<int>("OtpSettings:OtpExpireInSecounds")));


        }
    }
}
