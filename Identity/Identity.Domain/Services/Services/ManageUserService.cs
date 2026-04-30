using AutoMapper;
using Identity.Identity.Domain.IRepositories;
using Identity.Identity.Domain.IUnitOfWork;
using Identity.Identity.Domain.Models;
using Identity.Identity.Domain.Services.IServices;
using Microsoft.EntityFrameworkCore;
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
    public class ManageUserService(IUserRepository _user, IStringLocalizer<SharedResource> _localizer,
        IPasswordService _passwordService, IRoleRepository _roleRepo,
        IMapper _mapper, ICurrentUserService _currentUserService,
        IIdentityMouduleUoW _UoW) : IManageUserService

    {
        public async Task<ResponseModel<bool>> AddUser(AddNewUserDTO model)
        {
            var userExists = await _user.CheckUserExistsAsync(model);

            if (userExists)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserAlreadyExists"]
                };
            }

            var roleExists = await _roleRepo.CheckRoleExistsAsync(model.RoleId);

            if (!roleExists)
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["CheckRole"]
                };
            }
            //model.UserName.Trim();
            //var newUser = _mapper.Map<User>(model);
            //newUser.CreatedUser = _currentUserService.UserId;
            //newUser.Password = _passwordService.Hash(model.Password);


            var newUser = User.Create(model.UserName, model.Email, _passwordService.Hash(model.Password), model.FullName, model.RoleId, _currentUserService.UserId, model.phonenumber);


            await _user.Add(newUser);
            await _UoW.SaveChangesAsync();

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["UserAddedSuccessfully"]
            };
        }


        public async Task<ResponseModel<PagedResult<UserInfoDTO>>> GetUsers(GetUsersRequest model)
        {
            var result = await _user.GetUsersAsync(model, x => x.Include(x => x.Role), false);


            return new ResponseModel<PagedResult<UserInfoDTO>> { Success = true, Data = result, Message = _localizer["DataRetunedSuccssefully"] };
        }
    }
}
