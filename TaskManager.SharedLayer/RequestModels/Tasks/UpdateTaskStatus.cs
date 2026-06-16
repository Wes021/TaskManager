namespace TaskManager.SharedLayer.RequestModels.Tasks
{
    public class UpdateTaskStatus
    {
        public int TaskId { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public int NewStatus { get; set; }
    }
}
