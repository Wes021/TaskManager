using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.ResponseModels
{
    public class JwtTokenData
    {
        public int Id { get; set; }
     

        public string Role { get; set; }

        public int RoleId { get; set; }

    }
}
