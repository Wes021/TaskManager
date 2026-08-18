namespace TaskManager.SharedLayer.Interfaces
{
    public interface IProjectAuthorizationService
    {
        Task<bool> IsMemberAsync(int projectID, int userId);

        Task<bool> IsLeaderAsync(int projectID, int userId);

        Task<bool> CanManageProject(int projectID, int userId);

        Task<bool> CanLeaveProject(int projectID, int userId);

        Task<bool> CanTransferLeadership(int projectID, int userId);
    }
}
