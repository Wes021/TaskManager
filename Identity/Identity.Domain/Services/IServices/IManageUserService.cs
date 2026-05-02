using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Domain.Services.IServices
{
    public interface IManageUserService
    {
        Task<ResponseModel<PagedResult<UserInfoDTO>>> GetUsers(GetUsersRequest model);

        Task<ResponseModel<bool>> AddUser(AddNewUserDTO model);

        Task<ResponseModel<bool>> UpdateUserStatus(int Id,UpdateUserStatus model);

        Task<ResponseModel<bool>> DeleteUser(int Id, UpdateUserStatus model);


    }
}
