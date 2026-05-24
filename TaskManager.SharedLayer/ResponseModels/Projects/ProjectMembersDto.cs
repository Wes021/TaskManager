using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.ResponseModels.Projects
{
    public class ProjectMembersDto
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
