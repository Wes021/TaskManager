using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaskManager.SharedLayer.Enums.SystemEnums;

namespace TaskManager.SharedLayer.ResponseModels.Projects
{
    public class ProjectInfoDto
    {
        public int Id { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }

        public DateTime StartDate { get;  set; }
        public DateTime? EndDate { get;  set; }

        public string Manager { get; set; }
        public int ManagerId { get;  set; }

        public string Status { get;  set; }
        

        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public List<ProjectMembersDto> ProjectMembers { get; set; }




    }
}
