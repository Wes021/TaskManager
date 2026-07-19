using Identity.Identity.Domain.IRepositories;
using Identity.Identity.Domain.Models;
using Module.Identity.Infrastructure.DbSettings;

namespace Identity.Identity.Infrastructure.Repositories
{
    public class GeneratedOTPRepository : IGeneratedOTPRepository
    {
        private readonly IdentityDbContext _context;

        public GeneratedOTPRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<GeneratedOTP> Add(GeneratedOTP entity)
        {
            _context.Add(entity);
            return entity;
        }
    }
}
