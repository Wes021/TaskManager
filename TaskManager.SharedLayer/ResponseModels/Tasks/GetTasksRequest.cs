namespace TaskManager.SharedLayer.ResponseModels.Tasks
{
    public class GetTasksRequest
    {


        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }
        public string? Role { get; set; }
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public string SortBy { get; set; } = "CreatedDate";
        public string SortDir { get; set; } = "desc";
    }
}
