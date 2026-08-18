using Projects.Projects.Domain.IRepositories;
using TaskManager.SharedLayer.Enums;
using TaskManager.SharedLayer.Interfaces;

namespace Projects.Projects.Domain.Services.Services
{
    public class ProjectAuthorizationService(IProjectMemberRepository _projectMemberRepository) : IProjectAuthorizationService
    {
        public async Task<bool> CanLeaveProject(int projectID, int userId)
        {
            var result = await _projectMemberRepository.GetAssignedUserIdAsync(projectID, userId);

            if (result.ProjectMemberRole.Id != (int)SystemEnums.ProjectMemberRole.Leader)
            {
                return true;
            }

            return false;
        }

        public Task<bool> CanManageProject(int projectID, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanTransferLeadership(int projectID, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsLeaderAsync(int projectID, int userId)
        {
            var result = await _projectMemberRepository.GetAssignedUserIdAsync(projectID, userId);

            if (result.ProjectMemberRole.Id == (int)SystemEnums.ProjectMemberRole.Leader)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsMemberAsync(int projectID, int userId)
        {
            var result = await _projectMemberRepository.GetAssignedUserIdAsync(projectID, userId);

            if (result is null)
            {
                return false;
            }

            return true;
        }
    }
}
