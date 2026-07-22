using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Application.Handlers.IHandlers
{
    public interface IUsersHandlers
    {
        Task<ResponseModel<PagedResult<UserInfoDTO>>> GetUserAsync(GetUsersRequest model);


        Task<ResponseModel<bool>> AddUserAsync(AddNewUserDTO model);


        Task<ResponseModel<bool>> UpdateUserStatusAsync(int Id, UpdateUserStatus model);

        Task<ResponseModel<bool>> DeleteUserAsync(int Id, UpdateUserStatus model);


        Task<ResponseModel<bool>> UpdateUserPassword(UpdateUserPassword model);


    }
}
