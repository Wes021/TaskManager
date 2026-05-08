using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.RequestModels.Identity
{
    public class UserLookupDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
