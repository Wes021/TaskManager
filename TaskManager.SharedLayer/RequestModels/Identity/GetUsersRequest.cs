using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.RequestModels.Identity
{
    public class GetUsersRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }
        public string? Role { get; set; }
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }

        public string SortBy { get; set; } = "CreatedDate";
        public string SortDir { get; set; } = "desc";
    }
}
