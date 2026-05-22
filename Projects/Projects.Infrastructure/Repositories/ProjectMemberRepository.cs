using Microsoft.EntityFrameworkCore;
using Module.Projects.Infrastructure.DbSettings;
using Projects.Projects.Domain.IRepositories;
using Projects.Projects.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Projects.Infrastructure.Repositories
{
    public class ProjectMemberRepository : IProjectMemberRepository
    {
        private readonly ProjectsDbContext _context;

        public ProjectMemberRepository(ProjectsDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectMember>> GetAssignedUserIdsAsync(List<int> userIds)
        {
            return await _context.ProjectMember
     .AsNoTracking()
     .Include(x => x.Project)
     .Where(x =>
         userIds.Contains(x.UserId) &&
         !x.Project.IsDeleted &&
         x.Project.IsActive &&
         x.Project.EndDate > DateTime.UtcNow)
     .ToListAsync();
        }

        public async Task<ProjectMember> GetProjectByMemberIdAsync(int Id, Func<IQueryable<ProjectMember>, IQueryable<ProjectMember>>? include = null, bool isTracked = true)
        {
            IQueryable<ProjectMember> query = _context.ProjectMember;

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);


            return await query.FirstOrDefaultAsync(x => x.UserId == Id);
        }

        public async Task<List<ProjectMember>> GetProjectByProjectIdAsync(int Id, Func<IQueryable<ProjectMember>, IQueryable<ProjectMember>>? include = null, bool isTracked = true)
        {
            IQueryable<ProjectMember> query = _context.ProjectMember;

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);


            return await query.Where(x => x.ProjectId == Id).ToListAsync();
        }
    }
}
