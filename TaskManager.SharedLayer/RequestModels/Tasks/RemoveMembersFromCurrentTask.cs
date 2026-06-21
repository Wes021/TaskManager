namespace TaskManager.SharedLayer.RequestModels.Tasks
{
    public class RemoveMembersFromCurrentTask
    {
        public int TaskId { get; set; }

        public AddMembersToTask MembersModels { get; set; }
    }
}
