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

        public async Task<ProjectMember> Add(ProjectMember entity)
        {
            _context.ProjectMember.Add(entity);

            return entity;
        }

        public async Task<List<ProjectMember>> AddRange(List<ProjectMember> entities)
        {
            await _context.ProjectMember.AddRangeAsync(entities);
            return entities;
        }

        public async Task<List<ProjectMember>> GetAssignedUserIdsAsync(int projectId, List<int> userIds)
        {
            return await _context.ProjectMember
     .AsNoTracking()

     .Where(x =>
         userIds.Contains(x.UserId) &&
         x.ProjectId != projectId &&
x.IsDeleted == false &&
x.IsActive == true
)
     .ToListAsync();
        }

        public async Task<List<ProjectMember>> GetAssignedUserIdsWithProjectIdAsync(int projectId, List<int> userIds, bool isTracked = true)
        {
            IQueryable<ProjectMember> query =
        _context.ProjectMember;

            if (!isTracked)
                query = query.AsNoTracking();

            return await query
                .Where(x =>
                    userIds.Contains(x.UserId) &&
                    x.ProjectId == projectId &&
                    !x.IsDeleted &&
                    x.IsActive)
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
