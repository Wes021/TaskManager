using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.RequestModels.Projects;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Projects;

namespace Projects.Projects.Domain.Services.IServices
{
    public interface IProjectService
    {
        Task<ResponseModel<bool>> AddProject(CreateProjectDto model);


        Task<ResponseModel<PagedResult<ProjectInfoDto>>> GetProjects(GetProjectsRequest model);
    }
}
