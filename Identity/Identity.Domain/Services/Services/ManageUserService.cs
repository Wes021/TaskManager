using Identity.Identity.Domain.Services.IServices;
using Microsoft.Extensions.Localization;
using Module.Identity.Domain.IRepositories;
using Module.Identity.Domain.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Domain.Services.Services
{
    public class ManageUserService(IUserRepository _user, IStringLocalizer<SharedResource> _localizer, IPasswordService _passwordService) : IManageUserService
    {
        public async Task<ResponseModel<bool>> AddUser(AddNewUserDTO model)
        {
            var CheckUniqueness =await _user.CheckUserExistsAsync(model);

            if (CheckUniqueness != false)
                return new ResponseModel<bool> { Success = true, Data = false, Message = _localizer["UserAlreadyExists"] };


            throw new NotImplementedException();
        }


        public async Task<ResponseModel<PagedResult<UserInfoDTO>>> GetUsers(GetUsersRequest model)
        {
            var result = await _user.GetUsersAsync(model, null, false);

            
            return new ResponseModel<PagedResult<UserInfoDTO>> { Success = true, Data = result, Message = _localizer["DataRetunedSuccssefully"] };
        }
    }
}
