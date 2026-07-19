using TaskManager.SharedLayer.RequestModels;
using TaskManager.SharedLayer.ResponseModel;

namespace TaskManager.SharedLayer.Interfaces
{
    public interface IEmailService
    {
        Task<ResponseModel<bool>> SendEmail(SendEmailRequest emailRequest);
    }
}
