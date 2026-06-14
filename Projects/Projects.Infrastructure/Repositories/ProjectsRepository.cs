using Microsoft.EntityFrameworkCore;
using Module.Projects.Infrastructure.DbSettings;
using Projects.Projects.Domain.IRepositories;
using Projects.Projects.Domain.Models;
using TaskManager.SharedLayer.RequestModels.Projects;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Projects;

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

        public async Task<bool> ExistsByNameAsync(CreateProjectDto entity)
        {
            return await _context.Project.AsNoTracking()
                .AnyAsync(x => x.Name == entity.Name && x.IsDeleted != true);

        }



        public async Task<PagedResult<ProjectInfoDto>> GetProjectsAsync(GetProjectsRequest request, Func<IQueryable<Project>, IQueryable<Project>>? include = null, bool isTracked = true)
        {
            IQueryable<Project> query = _context.Project;

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);


            //Use Later For Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search)
                );
            }


            if (request.IsActive.HasValue)
                query = query.Where(x => x.IsActive == request.IsActive.Value);


            //Sort by
            query = request.SortDir == "asc"
                ? query.OrderBy(x => x.CreatedDate)
                : query.OrderByDescending(x => x.CreatedDate);


            var totalCount = await query.CountAsync();


            // pagination
            var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ProjectInfoDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ManagerId = x.ManagerId,
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status.Name,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,
                CreatedUserId = x.CreatedUser.Value

            })
            .ToListAsync();


            return new PagedResult<ProjectInfoDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
        }

        public async Task<Project> GetProjectByIdAsync(int Id, Func<IQueryable<Project>, IQueryable<Project>>? include = null, bool isTracked = true)
        {

            IQueryable<Project> query = _context.Project;

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);


            return await query.FirstOrDefaultAsync(x => x.Id == Id);
        }



        public async Task<List<ProjectInfoDto>> GetProjectsByIdsAsync(
    List<int> projectIds)
        {
            return await _context.Project
                .AsNoTracking()
                .Where(x =>
                    projectIds.Contains(x.Id) &&
                    !x.IsDeleted &&
                    x.IsActive)
                .Select(x => new ProjectInfoDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }
}
