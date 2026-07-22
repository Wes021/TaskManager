using Identity.Identity.Application.Handlers.IHandlers;
using Identity.Identity.Domain.Services.IServices;
using Microsoft.Extensions.Localization;
using Module.Identity.Domain.IRepositories;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Application.Handlers.Handlers
{
    public class UsersHandlers(IManageUserService _userService, IUserRepository _userRepo, IStringLocalizer<SharedResource> _localizer) : IUsersHandlers
    {
        public async Task<ResponseModel<bool>> AddUserAsync(AddNewUserDTO model)
        {
            var response = await _userService.AddUser(model);

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteUserAsync(int Id, UpdateUserStatus model)
        {
            var userId = Id;
            var user = await _userRepo.GetById(userId, null, false);


            if (user == null)
                return new ResponseModel<bool> { Success = false, Message = _localizer["UserNotFound"] };

            var users = await _userService.DeleteUser(Id, model);

            return users;

        }

        public async Task<ResponseModel<PagedResult<UserInfoDTO>>> GetUserAsync(GetUsersRequest model)
        {
            var users = await _userService.GetUsers(model);

            return users;
        }

        public Task<ResponseModel<bool>> UpdateUserPassword(UpdateUserPassword model)
        {
            if (model.)
                throw new NotImplementedException();
        }

        public async Task<ResponseModel<bool>> UpdateUserStatusAsync(int Id, UpdateUserStatus model)
        {

            var userId = Id;
            var user = await _userRepo.GetById(userId, null, false);



            var users = await _userService.UpdateUserStatus(Id, model);

            return users;
        }
    }
}
