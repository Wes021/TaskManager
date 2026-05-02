using Projects.Projects.Application.Handlers.IHandler;
using Projects.Projects.Domain.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.RequestModels.Projects;
using TaskManager.SharedLayer.ResponseModel;

namespace Projects.Projects.Application.Handlers.Handler
{
    public class ProjectHandler(IProjectService _projectService) : IProjectHandler
    {
        public async Task<ResponseModel<bool>> AddProjectAsync(CreateProjectDto model)
        {
            var project = await _projectService.AddProject(model);

            return project;
            
        }
    }
}
