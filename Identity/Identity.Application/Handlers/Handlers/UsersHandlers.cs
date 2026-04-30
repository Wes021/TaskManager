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

        public async Task<ResponseModel<PagedResult<UserInfoDTO>>> GetUserAsync(GetUsersRequest model)
        {
            var users = await _userService.GetUsers(model);
            return new ResponseModel<PagedResult<UserInfoDTO>>
            {
                Data = users.Data,
                Success = users.Success,
                Message = users.Message
            };
        }
    }
}
