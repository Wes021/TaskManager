using AutoMapper;
using Identity.Identity.Application.Handlers.IHandlers;
using Microsoft.AspNetCore.Identity;
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

namespace Identity.Identity.Application.Handlers.Handlers
{
    public class LoginHandler(IUserRepository _userRepo, IJwtService _jwt,
         IStringLocalizer<SharedResource> _localizer,
         IPasswordService _hasher, IMapper _mapper) : ILoginHandler
    {
        

        public async Task<ResponseModel<LoginResponseDTO>> Handle(LoginModel model)
        {
            var user = await _userRepo.GetByUsername(model.Username, x => x.Include(r => r.Role));

            if (user == null)
                return new ResponseModel<LoginResponseDTO> { Success = false, Message = _localizer["WrongUserNameOrPassword"], Data = null };


            if (!_hasher.Verify(model.Password, user.Password))
                return new ResponseModel<LoginResponseDTO> { Success = false, Message = _localizer["WrongUserNameOrPassword"], Data = null };


            if (!user.CanLogin())
                return new ResponseModel<LoginResponseDTO> { Success = false, Message = _localizer["SomthingWentWrong"], Data = null };


            if (!user.HasRole())
                return new ResponseModel<LoginResponseDTO> { Success = false, Message = _localizer["SomthingWentWrong"], Data = null };




            var mapperData = _mapper.Map<JwtTokenData>(user);

            var loginResponse = new LoginResponseDTO
            {
                Token = _jwt.GenerateToken(mapperData),
            };


            return new ResponseModel<LoginResponseDTO> { Success = true, Message = _localizer["LoginSuccessful"], Data = loginResponse };




        }
    }
}
