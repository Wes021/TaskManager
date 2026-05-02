using AutoMapper;
using Identity.Identity.Application.Handlers.IHandlers;
using Identity.Identity.Domain.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Module.Identity.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.Interfaces;
using TaskManager.SharedLayer.Localizer;
using TaskManager.SharedLayer.RequestModels;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;

namespace Identity.Identity.Application.Handlers.Handlers
{
    public class ProfileHandler(IUserRepository _userRepo,
        IMapper _mapper,
        IStringLocalizer<SharedResource> _localizer,
        ICurrentUserService _currentUser) : IProfileHandler
    {
        public async Task<ResponseModel<UserInfoDTO>> GetProfileAsync()
        {
            var userId = _currentUser.UserId;
            var user = await _userRepo.GetById(userId, x => x.Include(r => r.Role), false);
            

            if (user == null)
                return new ResponseModel<UserInfoDTO> { Success = false, Message = _localizer["UserNotFound"], Data = null };

            var mappedData = _mapper.Map<UserInfoDTO>(user);

            return new ResponseModel<UserInfoDTO> { Success = true, Message = _localizer["RetrivedSuccessfully"], Data = mappedData };
        }
    }
}
