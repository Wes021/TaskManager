using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.RequestModels.Tasks
{
    public class AddMembersToTask
    {
        public List<int> MemberIds { get; set; } = [];
    }
}
