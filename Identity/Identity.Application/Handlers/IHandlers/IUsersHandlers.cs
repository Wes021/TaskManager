using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Application.Handlers.IHandlers
{
    public interface IUsersHandlers
    {
        Task<ResponseModel<PagedResult<UserInfoDTO>>> GetUserAsync(GetUsersRequest model);


        Task<ResponseModel<bool>> AddUserAsync(AddNewUserDTO model);


    }
}
