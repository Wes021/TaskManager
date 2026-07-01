namespace TaskManager.SharedLayer.Interfaces
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }

        int UserId { get; }

        string UserName { get; }

        string Name { get; }

        string Role { get; }

        string? Email { get; }

        IEnumerable<string> Roles { get; }
    }
}
