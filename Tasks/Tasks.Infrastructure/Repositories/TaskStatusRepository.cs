using Microsoft.EntityFrameworkCore;
using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Infrastructure.DbSettings;

namespace Tasks.Tasks.Infrastructure.Repositories
{
    public class TaskStatusRepository : ITaskStatusRepository
    {
        private readonly TasksDbContext _context;

        public TaskStatusRepository(TasksDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckTaskStatusExists(int StatusId)
        {
            return await _context.TasksStatus.AsNoTracking()
    .AnyAsync(x => x.Id == StatusId && x.IsDeleted != true);
        }
    }
}
