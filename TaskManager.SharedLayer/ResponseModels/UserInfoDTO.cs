using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.SharedLayer.ResponseModels
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
 
        public DateTime? ModifiedDate { get; set; }
    }
}
