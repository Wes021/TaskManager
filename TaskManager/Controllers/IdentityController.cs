using Identity.Identity.Application.Handlers.IHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.SharedLayer.RequestModels.Identity;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(ILoginHandler _loginHandler, IProfileHandler _profileHandler, IUsersHandlers _userHnadler) : ControllerBase
    {

        [HttpPost("/api/v1/auth/login")]
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
        [HttpPatch("/api/users/{id}/status")]
        public async Task<IActionResult> UserStatus(int id ,UpdateUserStatus request)
        {
            var result = await _userHnadler.UpdateUserStatusAsync(id,request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("/api/users/{id}")]
        public async Task<IActionResult> DeleteUser(int id ,UpdateUserStatus request)
        {
            var result = await _userHnadler.DeleteUserAsync(id,request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }
    }
}
