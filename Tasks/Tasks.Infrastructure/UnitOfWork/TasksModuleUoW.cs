using Tasks.Tasks.Domain.IUnitOfWork;
using Tasks.Tasks.Infrastructure.DbSettings;

namespace Tasks.Tasks.Infrastructure.UnitOfWork
{
    public class TasksModuleUoW : ITasksModuleUoW
    {
        private readonly TasksDbContext _context;

        public TasksModuleUoW(TasksDbContext context)
        {
            _context = context;
        }
        public Task<int> SaveChangesAsync(
       CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
