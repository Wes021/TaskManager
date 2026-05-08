using Projects.Projects.Application.Handlers.IHandler;
using Projects.Projects.Domain.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Projects;
using TaskManager.SharedLayer.ResponseModel;
using TaskManager.SharedLayer.ResponseModels;
using TaskManager.SharedLayer.ResponseModels.Projects;

namespace Projects.Projects.Application.Handlers.Handler
{
    public class ProjectHandler(IProjectService _projectService) : IProjectHandler
    {
        public async Task<ResponseModel<bool>> AddProjectAsync(CreateProjectDto model)
        {
            var project = await _projectService.AddProject(model);

            return project;
            
        }

        public async Task<ResponseModel<PagedResult<ProjectInfoDto>>> GetProjectsAsync(GetProjectsRequest model)
        {
            var project = await _projectService.GetProjects(model);

            return project;
        }
    }
}
