using Microsoft.AspNetCore.Http;

namespace TaskManager.SharedLayer.RequestModels.Tasks.TasksModels
{
    public class NewTaskRequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }



        public DateTime DueDate { get; set; }




        public int ProjectId { get; set; }

        public int TaskStatus { get; set; }

        public AddMembersToTask MembersModel { get; set; }

        public List<IFormFile>? Files { get; set; }



    }
}
