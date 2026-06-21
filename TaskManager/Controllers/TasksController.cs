using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.SharedLayer.RequestModels.Tasks;
using TaskManager.SharedLayer.ResponseModels.Tasks;
using Tasks.Tasks.Application.Handlers.IHandlers;
using Tasks.Tasks.Domain.Services.IServices;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITasksHandler _tasksHandler, ITaskComments _taskComments) : ControllerBase
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
        [HttpGet("/api/v1/tasks/assigned-to-me")]
        public async Task<IActionResult> GetUserTask([FromQuery] GetTasksRequest request)
        {
            var result = await _tasksHandler.GetTasksByCurrentUserId(request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("/api/v1/projects/{ProjectId}/tasks")]
        public async Task<IActionResult> GetProjectTask([FromQuery] GetTasksRequest request, int ProjectId)
        {
            var result = await _tasksHandler.GetTasksByProjectID(request, ProjectId);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("/api/v1/tasks/{Id}")]
        public async Task<IActionResult> GetTaskById(int Id)
        {
            var result = await _tasksHandler.GetTaskById(Id);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch("/api/v1/tasks/{taskId}/Status")]
        public async Task<IActionResult> UpdateTaskStatus(int taskId, [FromBody] UpdateTaskStatus request)
        {

            var result = await _tasksHandler.SetTaskStatus(taskId, request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("/api/v1/tasks/{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId, [FromBody] DeleteTask request)
        {

            var result = await _tasksHandler.DeleteTask(taskId, request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }


        [Authorize]
        [HttpPost("/api/v1/tasks/{taskId}/Members")]
        public async Task<IActionResult> AddMembers(int taskId, [FromForm] AddMembersToCurrentTask request)
        {
            request.TaskId = taskId;
            var result = await _tasksHandler.AddMembersToTask(request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("/api/v1/tasks/{taskId}/Members")]
        public async Task<IActionResult> RemoveMembers(int taskId, [FromForm] RemoveMembersFromCurrentTask request)
        {
            request.TaskId = taskId;
            var result = await _tasksHandler.RemoveMembersFromTask(request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }



        [Authorize]
        [HttpPost("/api/v1/tasks/{taskId}/comment")]
        public async Task<IActionResult> AddComment(int taskId, [FromForm] AddNewComment request)
        {

            var result = await _taskComments.AddNewComment(taskId, request);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }
    }
}
