namespace TaskManager.SharedLayer.Enums
{
    public class SystemEnums
    {
        public enum UserType
        {
            Employee = 1,
            Admin = 2,
            ManagerAndLeader = 3

        }

        public enum PolicyKeywords
        {
            PasswordReset = 1,


        }


        public enum ProjectStatus
        {
            Draft = 1,
            Active = 2,
            OnHold = 3,
            Completed = 4,
            Cancelled = 5
        }

        public enum TaskStatuseEnums
        {
            Draft = 1,
            Active = 2,
            Completed = 3,
            Reopen = 4,
            Cancelled = 5
        }

        public enum TaskHistoryActions
        {
            CreatedNewTask,
            UpdatedTheTask,
            AddedNewMembers,
            RemovedMember,
            UpdatedTheStatus,
            DeletedTheTask,
            AddedNewComment,
            DeletedAComment
        }

        public enum ProjectMemberRole
        {
            Leader = 1,
            Member = 2
        }


    }
}
