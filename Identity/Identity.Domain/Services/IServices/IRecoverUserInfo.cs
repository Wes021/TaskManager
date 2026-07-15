using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.ResponseModel;

namespace Identity.Identity.Domain.Services.IServices
{
    public interface IRecoverUserInfo
    {
        Task<ResponseModel<bool>> ForgotPassword(ForgotPasswordDTO model);
    }
}
