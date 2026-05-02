using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.RequestModels.Identity
{
    public class LoginModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
