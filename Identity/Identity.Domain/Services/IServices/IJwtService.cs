using TaskManager.SharedLayer.ResponseModels;

namespace Module.Identity.Domain.Services.IServices
{
    public interface IJwtService
    {
        string GenerateToken(JwtTokenData userModel);

        string GenerateResetPasswordToken(int userId);



    }
}
