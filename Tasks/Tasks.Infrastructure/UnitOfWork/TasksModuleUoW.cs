using Tasks.Tasks.Domain.IUnitOfWork;

namespace Tasks.Tasks.Infrastructure.UnitOfWork
{
    public class TasksModuleUoW : ITasksModuleUoW
    {
        private readonly TasksModuleUoW _context;

        public TasksModuleUoW(TasksModuleUoW context)
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
