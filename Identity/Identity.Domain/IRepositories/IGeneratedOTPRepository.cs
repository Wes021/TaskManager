using Identity.Identity.Domain.Models;
using TaskManager.SharedLayer.RequestModels.Identity;

namespace Identity.Identity.Domain.IRepositories
{
    public interface IGeneratedOTPRepository
    {
        Task<GeneratedOTP> Add(GeneratedOTP entity);

        Task<GeneratedOTP?> GetGeneratedOTPAsync(GetOtpSearchDto email, Func<IQueryable<GeneratedOTP>, IQueryable<GeneratedOTP>>? include = null, bool isTracked = true);
    }
}
