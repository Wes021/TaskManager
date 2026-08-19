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

        public enum TaskHistoryAction
        {
            Created = 1,
            TaskUpdated = 2,
            StatusChanged = 3,

            MemberAdded = 4,
            MemberRemoved = 5,

            CommentAdded = 6,

            AttachmentAdded = 7,
            AttachmentRemoved = 8,
            CommentDeleted = 9,
            TaskDeleted = 10,
            TaskAdded = 11,
            StatusUpdated = 12
        }

        public enum ProjectMemberRole
        {
            Leader = 1,
            Member = 2
        }


    }
}
