using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.RequestModels
{
    public class UpdateUserStatus
    {
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; } = true;
    }
}
