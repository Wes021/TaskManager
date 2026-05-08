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

namespace Projects.Projects.Application.Handlers.IHandler
{
    public interface IProjectHandler
    {
        Task<ResponseModel<bool>> AddProjectAsync(CreateProjectDto model);

        Task<ResponseModel<PagedResult<ProjectInfoDto>>> GetProjectsAsync(GetProjectsRequest model);

        Task<ResponseModel<ProjectInfoDto>> GetProjectByIdAsync(int Id);

        Task<ResponseModel<bool>> UpdateProjectStatusAsync(int Id, UpdateProjectStatus model);

        Task<ResponseModel<bool>> DeleteProjectAsync(int Id, UpdateProjectStatus model);
    }
}
