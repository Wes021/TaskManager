namespace TaskManager.SharedLayer.ResponseModels.Tasks
{
    public class TaskMembersDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string phonenumber { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
