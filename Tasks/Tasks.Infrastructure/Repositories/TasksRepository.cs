using Microsoft.EntityFrameworkCore;
using TaskManager.SharedLayer.RequestModels.Tasks.TasksModels;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Tasks;
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

        public async Task<bool> ExistsByTitleAsync(NewTaskRequestModel entity)
        {
            return await _context.Task.AsNoTracking()
                .AnyAsync(x => x.Title == entity.Title && x.IsDeleted != true);
        }

        public async Task<bool> ExistsByTitleForUpdateAsync(int TaskId, UpdateTaskInfo entity)
        {
            return await _context.Task.AsNoTracking()
                .AnyAsync(x => x.Title == entity.Title && x.IsDeleted != true && x.Id != TaskId);
        }

        public Task<Domain.Models.Tasks> GetTaskById(int TaskId, Func<IQueryable<Domain.Models.Tasks>, IQueryable<Domain.Models.Tasks>>? include = null, bool isTracked = true)
        {
            IQueryable<Domain.Models.Tasks> query = _context.Task.Where(x => x.IsDeleted != true).Include(x => x.TasksStatus).Include(x => x.TaskAttachments.Where(att => !att.IsDeleted)).Include(x => x.Members.Where(me => !me.IsDeleted)).Include(x => x.TaskComments.Where(com => !com.IsDeleted));

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);


            return query.FirstOrDefaultAsync(x => x.Id == TaskId);


        }

        public async Task<PagedResult<TaskInfoDto>> GetTasksByProjectIdAsync(GetTasksRequest request, int ProjectId, Func<IQueryable<Domain.Models.Tasks>, IQueryable<Domain.Models.Tasks>>? include = null, bool isTracked = true)
        {
            IQueryable<Domain.Models.Tasks> query = _context.Task.Where(x => x.ProjectId == ProjectId && x.IsDeleted != true).Include(x => x.Members);

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);


            //Use Later For Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Title.Contains(request.Search)
                );
            }


            if (request.IsActive.HasValue)
                query = query.Where(x => x.IsActive == request.IsActive.Value);


            if (request.IsDeleted.HasValue)
                query = query.Where(x => x.IsDeleted == request.IsDeleted.Value);


            //Sort by
            query = request.SortDir == "asc"
                ? query.OrderBy(x => x.CreatedDate)
                : query.OrderByDescending(x => x.CreatedDate);


            var totalCount = await query.CountAsync();





            // pagination
            var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new TaskInfoDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DueDate = x.DueDate,
                TasksStatus = x.TasksStatus.Name,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,
                ProjectId = x.ProjectId,
                CreatedDate = x.CreatedDate,



            })
            .ToListAsync();


            return new PagedResult<TaskInfoDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };

        }

        public async Task<PagedResult<TaskInfoDto>> GetTasksByUserIdAsync(GetTasksRequest request, int UserId, Func<IQueryable<Domain.Models.Tasks>, IQueryable<Domain.Models.Tasks>>? include = null, bool isTracked = true)
        {
            IQueryable<Domain.Models.Tasks> query = _context.Task.Where(x => x.Members.Any(m => m.UserId == UserId) && x.IsDeleted != true);

            if (!isTracked)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);


            //Use Later For Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Title.Contains(request.Search)
                );
            }


            if (request.IsActive.HasValue)
                query = query.Where(x => x.IsActive == request.IsActive.Value);


            if (request.IsDeleted.HasValue)
                query = query.Where(x => x.IsDeleted == request.IsDeleted.Value);


            //Sort by
            query = request.SortDir == "asc"
                ? query.OrderBy(x => x.CreatedDate)
                : query.OrderByDescending(x => x.CreatedDate);


            var totalCount = await query.CountAsync();





            // pagination
            var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new TaskInfoDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DueDate = x.DueDate,
                TasksStatus = x.TasksStatus.Name,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,
                ProjectId = x.ProjectId,
                CreatedDate = x.CreatedDate,



            })
            .ToListAsync();


            return new PagedResult<TaskInfoDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };


        }
    }
}
