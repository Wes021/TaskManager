using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.Projects.Application.Handlers.IHandler;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.RequestModels.Projects;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectHandler _projectHandler) : ControllerBase
    {
        [Authorize]
        [HttpPost("/api/v1/projects")]
        public async Task<IActionResult> AddProject([FromBody] CreateProjectDto model)
        {
            var result = await _projectHandler.AddProjectAsync(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("/api/v1/projects")]
        public async Task<IActionResult> GetProject([FromQuery] GetProjectsRequest model)
        {
            var result = await _projectHandler.GetProjectsAsync(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("/api/v1/projects/{Id}")]
        public async Task<IActionResult> GetProjectById( int Id)
        {
            var result = await _projectHandler.GetProjectByIdAsync(Id);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch("/api/v1/projects/{Id}/Status")]
        public async Task<IActionResult> UpdateProjectStatus(int Id, UpdateProjectStatus request)
        {
            var result = await _projectHandler.UpdateProjectStatusAsync(Id, request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("/api/v1/projects/{Id}")]
        public async Task<IActionResult> DeletProject(int Id, UpdateProjectStatus request)
        {
            var result = await _projectHandler.DeleteProjectAsync(Id, request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("/api/v1/projects/{Id}")]
        public async Task<IActionResult> UpdateProject(int Id, UpdateProjectInfo request)
        {
            var result = await _projectHandler.UpdateProjectAsync(Id, request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }
    }
}
