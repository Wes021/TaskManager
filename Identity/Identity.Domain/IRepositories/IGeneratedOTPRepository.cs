using Identity.Identity.Domain.Models;

namespace Identity.Identity.Domain.IRepositories
{
    public interface IGeneratedOTPRepository
    {
        Task<GeneratedOTP> Add(GeneratedOTP entity);
    }
}
