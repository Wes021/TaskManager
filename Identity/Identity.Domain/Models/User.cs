using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;

namespace Identity.Identity.Domain.Models
{
    public class User : IEntity, IAuditedEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }


        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }


        public DateTime CreatedDate { get; set; }
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }
        



        public bool CanLogin()
       => !IsDeleted && IsActive;

        public bool HasRole()
            => Role != null;
    }
}
