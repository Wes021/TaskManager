using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.RequestModels.Projects;
using TaskManager.SharedLayer.ResponseModel;

namespace Projects.Projects.Domain.Services.IServices
{
    public interface IProjectService
    {
        Task<ResponseModel<bool>> AddProject(CreateProjectDto model);
    }
}
