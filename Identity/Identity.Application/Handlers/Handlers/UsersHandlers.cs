using Identity.Identity.Application.Handlers.IHandlers;
using Identity.Identity.Domain.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Application.Handlers.Handlers
{
    public class UsersHandlers(IManageUserService _userService) : IUsersHandlers
    {
        public async Task<ResponseModel<bool>> AddUserAsync(AddNewUserDTO model)
        {
            var response = await _userService.AddUser(model);

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteUserAsync(int Id, UpdateUserStatus model)
        {
            var users = await _userService.DeleteUser(Id, model);

            return users;
            
        }

        public async Task<ResponseModel<PagedResult<UserInfoDTO>>> GetUserAsync(GetUsersRequest model)
        {
            var users = await _userService.GetUsers(model);

            return users;
        }

        public async Task<ResponseModel<bool>> UpdateUserStatusAsync(int Id, UpdateUserStatus model)
        {
            var users = await _userService.UpdateUserStatus(Id, model);

            return users;
        }
    }
}
