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
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ProjectsDbContext _context;

        public ProjectsRepository(ProjectsDbContext context)
        {
            _context = context;
        }

        public async Task<Project> Add(Project entity)
        {
            _context.Project.Add(entity);

            return entity;
        }

        public async Task<bool> CheckProjectExists(CreateProjectDto entity)
        {
            return await _context.Project.AsNoTracking()
                .AnyAsync(x => x.Name == entity.Name && x.IsDeleted != true);
            
        }
    }
}
