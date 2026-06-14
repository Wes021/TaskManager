namespace TaskManager.SharedLayer.ResponseModels.Tasks
{
    public class TaskInfoDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }



        public DateTime DueDate { get; set; }

        public string TasksStatus { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public DateTime CreatedDate { get; set; }


        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }



    }
}
