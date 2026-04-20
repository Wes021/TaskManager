using Identity.Identity.Application.Handlers.IHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.SharedLayer.RequestModels;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(ILoginHandler _loginHandler) : ControllerBase
    {

        [HttpPost("/api/v1/auth/login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _loginHandler.Handle(model);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}
