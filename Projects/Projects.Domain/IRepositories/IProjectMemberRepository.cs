using Projects.Projects.Domain.Models;

namespace Projects.Projects.Domain.IRepositories
{
    public interface IProjectMemberRepository
    {

        Task<List<ProjectMember>> AddRange(List<ProjectMember> entities);

        Task<List<ProjectMember>> GetProjectByProjectIdAsync(
             int Id,
             Func<IQueryable<ProjectMember>, IQueryable<ProjectMember>>? include = null,
             bool isTracked = true);


        Task<ProjectMember> GetProjectByMemberIdAsync(
             int Id,
             Func<IQueryable<ProjectMember>, IQueryable<ProjectMember>>? include = null,
             bool isTracked = true);


        Task<List<ProjectMember>> GetAssignedUserIdsAsync(int projectId, List<int> userIds);

        Task<List<ProjectMember>> GetAssignedUserIdsWithProjectIdAsync(int projectId, List<int> userIds, bool isTracked = true);







    }
}
