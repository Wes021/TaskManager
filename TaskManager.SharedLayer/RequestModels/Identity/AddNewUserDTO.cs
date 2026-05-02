using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.RequestModels.Identity
{
    public class AddNewUserDTO
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string phonenumber { get; set; }

        public int RoleId { get; set; }
    }
}
