using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModels.Tasks;
using Tasks.Tasks.Application.Handlers.IHandlers;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITasksHandler _tasksHandler) : ControllerBase
    {
        [Authorize]
        [HttpPost("/api/v1/tasks")]
        public async Task<IActionResult> AddTask([FromForm] NewTaskRequestModel request)
        {
            var result = await _tasksHandler.AddNewTask(request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("/api/v1/tasks")]
        public async Task<IActionResult> GetTask([FromQuery] GetTasksRequest request)
        {
            var result = await _tasksHandler.GetTasks(request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }
    }
}
