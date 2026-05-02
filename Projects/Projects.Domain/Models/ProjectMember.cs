using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Projects.Domain.Models
{
    public class ProjectMember
    {
        public int ProjectId { get; private set; }
        public int UserId { get; private set; }

        // Navigation (optional)
        public Project Project { get; set; }

        public ProjectMember(int projectId, int userId)
        {
            ProjectId = projectId;
            UserId = userId;
        }
    }
}
