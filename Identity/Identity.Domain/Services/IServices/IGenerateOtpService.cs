using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModel;

namespace Identity.Identity.Domain.Services.IServices
{
    public interface IGenerateOtpService
    {
        Task<ResponseModel<bool>> GenerateNewOtp(SendNewOtpDto model);
    }
}
