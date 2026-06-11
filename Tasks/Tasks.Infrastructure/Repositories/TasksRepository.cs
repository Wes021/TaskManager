using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Tasks.Domain.IRepositories;
using Tasks.Tasks.Infrastructure.DbSettings;

namespace Tasks.Tasks.Infrastructure.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly TasksDbContext _context;

        public TasksRepository(TasksDbContext context)
        {
            _context = context;
        }
        public async Task<Domain.Models.Tasks> Add(Domain.Models.Tasks entity)
        {
            await _context.Task.AddAsync(entity);

            return entity;
        }
    }
}
