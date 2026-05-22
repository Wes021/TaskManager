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
using TaskManager.SharedLayer.Enums;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels.Identity;
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

            var currectUserRole = _currentUserService.Role;

            if (currectUserRole != SystemEnums.UserType.Admin.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };


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

        public async Task<ResponseModel<bool>> UpdateUserStatus(int userId, UpdateUserStatus model)
        {
            var user = await _user.GetById(userId);
            var currentUserId = _currentUserService.UserId;
            var currectUserRole = _currentUserService.Role;

            if (user is null)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserDoesNotExist"]
                };

            if (currectUserRole != SystemEnums.UserType.Admin.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };




            var result = user.SetIsActive(model.IsActive, currentUserId);

            if (!result.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer[result.Error]
                };

            await _UoW.SaveChangesAsync();

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["UserUpdatedSuccessfully"]
            };
        }


        public async Task<ResponseModel<bool>> DeleteUser(int userId, UpdateUserStatus model)
        {
            var user = await _user.GetById(userId);
            var currentUserId = _currentUserService.UserId;
            var currectUserRole = _currentUserService.Role;

            if (currectUserRole != SystemEnums.UserType.Admin.ToString())
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer["UserNotAllowed"]
                };

            var result = user.SetIsDeleted(model.IsDeleted, currentUserId);

            if (!result.Succeeded)
                return new ResponseModel<bool>
                {
                    Success = false,
                    Data = false,
                    Message = _localizer[result.Error]
                };

            await _UoW.SaveChangesAsync();

            return new ResponseModel<bool>
            {
                Success = true,
                Data = true,
                Message = _localizer["UserDeletedSuccessfully"]
            };

        }

    }
}
