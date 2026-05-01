using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Domain.Models
{
    public class User : IEntity, IAuditedEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;


        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string phonenumber { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }

        private User() { }


        public static User Create(string userName, string email, string password, string fullName, int roleId, int createdUser, string phonenumber)
        {
            return new User
            {
                FullName = fullName,
                UserName = userName.Trim().Replace(" ", ""),
                Email = email.Trim().ToLower().Replace(" ", ""),
                Password = password,
                RoleId = roleId,
                phonenumber = phonenumber.Trim().Replace(" ", ""),
                CreatedUser = createdUser,
                CreatedDate = DateTime.Now,
                

            };
        }

        public DomainResponseModel SetIsActive(bool isActive, int modifiedUser)
        {
            if (IsActive == isActive)
                return DomainResponseModel.Fail("NoChangesDetected");

            if (Id == modifiedUser)
                return DomainResponseModel.Fail("CannotModifyOwnStatus");

            if (IsDeleted)
                return DomainResponseModel.Fail("DeletedUserStatusBlocked");

            IsActive = isActive;
            ModifiedDate = DateTime.UtcNow;
            ModifiedUser = modifiedUser;

            return DomainResponseModel.Success();
        }

        public DomainResponseModel SetIsDeleted(bool isDeleted, int modifiedUser)
        {
            if (IsDeleted == isDeleted)
                return DomainResponseModel.Fail("NoChangesDetected");

            if (!isDeleted)
                return DomainResponseModel.Fail("CantRestoreUser");

            if (Id == modifiedUser)
                return DomainResponseModel.Fail("CannotModifyOwnStatus");


            IsDeleted = isDeleted;
            IsActive = false;
            ModifiedDate = DateTime.UtcNow;
            ModifiedUser = modifiedUser;

            return DomainResponseModel.Success();
        }

        public bool CanLogin()
       => !IsDeleted && IsActive;

        public bool HasRole()
            => Role != null;



   



    }
}
