using Projects.Projects.Domain.Models;
using TaskManager.SharedLayer.RequestModels.Projects;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Projects;

namespace Projects.Projects.Domain.IRepositories
{
    public interface IProjectsRepository
    {
        Task<bool> ExistsByNameAsync(CreateProjectDto entity);

        Task<Project> Add(Project entity);

        Task<PagedResult<ProjectInfoDto>> GetProjectsAsync(
             GetProjectsRequest request,
             Func<IQueryable<Project>, IQueryable<Project>>? include = null,
             bool isTracked = true);

        Task<Project> GetProjectByIdAsync(
             int Id,
             Func<IQueryable<Project>, IQueryable<Project>>? include = null,
             bool isTracked = true);

        Task<List<ProjectInfoDto>> GetProjectsByIdsAsync(
    List<int> projectIds);
    }
}
