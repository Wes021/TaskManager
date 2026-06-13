using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Infrastructure.DbSettings;

namespace Tasks.Tasks.Infrastructure.Repositories
{
    public class UsersTasks : IUsersTasks
    {
        private readonly TasksDbContext _context;

        public UsersTasks(TasksDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Models.UsersTasks> Add(Domain.Models.UsersTasks entity)
        {

            _context.UsersTasks.Add(entity);

            return entity;
        }
    }
}
