using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.RequestModels.Projects;
using TaskManager.SharedLayer.ResponseModel;

namespace Projects.Projects.Application.Handlers.IHandler
{
    public interface IProjectHandler
    {
        Task<ResponseModel<bool>> AddProjectAsync(CreateProjectDto model);
    }
}
