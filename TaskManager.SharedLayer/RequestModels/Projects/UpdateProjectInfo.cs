using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.RequestModels.Projects
{
    public class UpdateProjectInfo
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public int ManagerId { get; private set; }
        public int StatusId { get; set; }
    }
}
