using Identity.Identity.Application.Handlers.IHandlers;
using Identity.Identity.Domain.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TaskManager.SharedLayer.RequestModels.Identity;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(ILoginHandler _loginHandler, IProfileHandler _profileHandler, IUsersHandlers _userHnadler, IGenerateOtpService _generateOtpService) : ControllerBase
    {

        [HttpPost("/api/v1/auth/login")]
        [EnableRateLimiting("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _loginHandler.Handle(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("/api/v1/users/profile")]
        public async Task<IActionResult> profile()
        {
            var result = await _profileHandler.GetProfileAsync();

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("/api/v1/users")]
        public async Task<IActionResult> Users([FromQuery] GetUsersRequest request)
        {
            var result = await _userHnadler.GetUserAsync(request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("/api/v1/users")]
        public async Task<IActionResult> Users(AddNewUserDTO request)
        {
            var result = await _userHnadler.AddUserAsync(request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch("/api/v1/users/{id}/status")]
        public async Task<IActionResult> UserStatus(int id, UpdateUserStatus request)
        {
            var result = await _userHnadler.UpdateUserStatusAsync(id, request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("/api/v1/users/{id}")]
        public async Task<IActionResult> DeleteUser(int id, UpdateUserStatus request)
        {
            var result = await _userHnadler.DeleteUserAsync(id, request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }


        [HttpPost("api/v1/auth/forgot-password/send-otp")]
        [EnableRateLimiting("Otp")]
        public async Task<IActionResult> SendNewOTP(SendNewOtpDto model)
        {
            var result = await _generateOtpService.GenerateNewOtp(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [HttpPost("api/v1/auth/forgot-password/validate-otp")]
        [EnableRateLimiting("VerifyOtp")]
        public async Task<IActionResult> ValidateOTP(ValidateOTPDto model)
        {
            var result = await _generateOtpService.ValidateOtp(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [HttpPost("api/v1/auth/forgot-password/update-password")]
        [EnableRateLimiting("UpdatePassword")]
        [Authorize(Policy = Policies.ResetPassword)]
        public async Task<IActionResult> ResetPassword(UpdateUserPassword model)
        {
            var result = await _userHnadler.UpdateUserPassword(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }
    }
}
