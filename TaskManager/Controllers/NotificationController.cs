using Microsoft.AspNetCore.Mvc;
using Notification.Notification.Application.Handlers.IHandlers;
using TaskManager.SharedLayer.RequestModels.Notification;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(INotificationsHandlers _notificationsHandlers) : ControllerBase
    {
        [HttpGet("/api/v1/Notifications")]

        public async Task<IActionResult> Notifications([FromQuery] GetNotificationForUserDTO model)
        {
            var result = await _notificationsHandlers.GetAllNotificationsByUserId(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }


        [HttpPost("/api/v1/Notification")]
        public async Task<IActionResult> AddNotification(NewCustomNotificationDTO model)
        {
            var result = await _notificationsHandlers.AddNewNotification(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }


        [HttpPost("/api/v1/bulk-notification")]
        public async Task<IActionResult> AddBulkNotification(NewCustomBulkNotificationDTO model)
        {
            var result = await _notificationsHandlers.AddNewBulkNotification(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }


        [HttpDelete("/api/v1/notification")]
        public async Task<IActionResult> DeleteNotification(DeleteNotificationDto model)
        {
            var result = await _notificationsHandlers.DeleteNotification(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [HttpDelete("/api/v1/delete-bulk-notification")]
        public async Task<IActionResult> DeleteBulkNotification(DeleteBulkNotifications model)
        {
            var result = await _notificationsHandlers.DeleteBulkNotification(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [HttpPatch("/api/v1/mark-notification-as-read")]
        public async Task<IActionResult> MarkNotificationAsRead(MarkAsReadRequestDTO model)
        {
            var result = await _notificationsHandlers.MarkOneNotificaitionAsRead(model);

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }

        [HttpPatch("/api/v1/mark-bulk-notification-as-read")]
        public async Task<IActionResult> MarkBulkNotificationAsRead()
        {
            var result = await _notificationsHandlers.MarkBulkNotificaitionAsRead();

            if (!result.Success)
                return Ok(result);

            return Ok(result);
        }



    }
}
