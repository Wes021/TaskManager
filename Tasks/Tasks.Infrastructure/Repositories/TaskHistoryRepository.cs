using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Domain.Models;
using Tasks.Tasks.Infrastructure.DbSettings;

namespace Tasks.Tasks.Infrastructure.Repositories
{
    public class TaskHistoryRepository : ITasksHistoryRepository
    {
        private readonly TasksDbContext _context;

        public TaskHistoryRepository(TasksDbContext context)
        {
            _context = context;
        }

        public async Task<TaskHistory> Add(TaskHistory entity)
        {
            await _context.TaskHistory.AddAsync(entity);

            return entity;

        }
    }
}
