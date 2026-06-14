namespace TaskManager.SharedLayer.ResponseModels.Tasks
{
    public class TaskAttachmentsDto
    {
        public int Id { get; set; }
        public int TasksId { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
