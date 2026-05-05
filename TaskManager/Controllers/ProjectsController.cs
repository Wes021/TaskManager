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

        [HttpPost("/api/v1/projects")]
        public async Task<IActionResult> AddProject([FromBody] CreateProjectDto model)
        {
            var result = await _projectHandler.AddProjectAsync(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }
    }
}
