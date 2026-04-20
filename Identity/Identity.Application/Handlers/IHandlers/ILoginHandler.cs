using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels;
using TaskManager.SharedLayer.ResponseModel;

namespace Identity.Identity.Application.Handlers.IHandlers
{
    public interface ILoginHandler
    {
        Task<ResponseModel<LoginResponseDTO>> Handle(LoginModel model);
    }
}
