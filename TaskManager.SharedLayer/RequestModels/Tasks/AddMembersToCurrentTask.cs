namespace TaskManager.SharedLayer.RequestModels.Tasks
{
    public class AddMembersToCurrentTask
    {

        public int TaskId { get; set; }

        public AddMembersToTask MembersModels { get; set; }
    }
}
