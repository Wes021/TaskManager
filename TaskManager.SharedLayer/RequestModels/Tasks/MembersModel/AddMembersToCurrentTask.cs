using TaskManager.SharedLayer.RequestModels.Tasks.TasksModels;

namespace TaskManager.SharedLayer.RequestModels.Tasks.MembersModel
{
    public class AddMembersToCurrentTask
    {

        public int TaskId { get; set; }

        public AddMembersToTask MembersModels { get; set; }
    }
}
