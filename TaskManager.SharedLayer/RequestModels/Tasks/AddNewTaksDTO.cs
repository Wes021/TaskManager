namespace TaskManager.SharedLayer.RequestModels.Tasks
{
    public class AddNewTaksDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }



        public DateTime DueDate { get; set; }


        public FileValidateRequest Files { get; set; }

        public int ProjectId { get; set; }

        public int ProjectStatus { get; set; }
    }
}
