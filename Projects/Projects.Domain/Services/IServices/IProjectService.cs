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


        Task<ResponseModel<ProjectInfoDto>> GetProjectById(int Id);


        Task<ResponseModel<bool>> DeleteProject(int ProjectId, UpdateProjectStatus model);

        Task<ResponseModel<bool>> UpdateProjectStatus(int ProjectId, UpdateProjectStatus model);

        Task<ResponseModel<bool>> UpdateProjectInfo(int ProjectId, UpdateProjectInfo model);

        Task<ResponseModel<bool>> AddMembersToProject(int ProjectId, AddProjectMembersDto model);

        Task<ResponseModel<bool>> RemoveMembersFromProject(int ProjectId, RemoveProjectMembersDto model);
    }
}
