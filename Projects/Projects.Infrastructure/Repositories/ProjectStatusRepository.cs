using Microsoft.EntityFrameworkCore;
using Module.Projects.Infrastructure.DbSettings;
using Projects.Projects.Domain.IRepositories;
using Projects.Projects.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Projects;

namespace Projects.Projects.Infrastructure.Repositories
{
    public class ProjectStatusRepository : IProjectStatusRepository
    {
        private readonly ProjectsDbContext _context;

        public ProjectStatusRepository(ProjectsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckProjectStatusExists(int StatusId)
        {
           
            return await _context.ProjectStatus.AsNoTracking()
                .AnyAsync(x => x.Id == StatusId && x.IsDeleted != true && x.IsActive != false);
            
        }
    }
}
