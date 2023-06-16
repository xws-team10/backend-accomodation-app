using Microsoft.AspNetCore.Mvc;
using notification_service.Model;
using notification_service.Service;

namespace notification_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<List<Notification>> Get() =>
           await _notificationService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> Get(Guid id)
        {
            var notification = await _notificationService.GetByIdAsync(id);

            if (notification is null)
                return NotFound();

            return notification;
        }

        [HttpGet("getByUser/{id}")]
        public async Task<List<Notification>> GetByUser(Guid id) =>
           await _notificationService.GetAllByUserAsync(id);

        [HttpPost]
        public async Task<IActionResult> Post(Notification newNotification)
        {
            await _notificationService.CreateAsync(newNotification);

            return CreatedAtAction(nameof(Get), new { id = newNotification.Id }, newNotification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Notification updateNotification)
        {
            var notification = await _notificationService.GetByIdAsync(id);

            if (notification is null)
                return NotFound();

            updateNotification.Id = notification.Id;

            await _notificationService.UpdateAsync(id, updateNotification);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var notification = await _notificationService.GetByIdAsync(id);

            if (notification is null)
                return NotFound();

            await _notificationService.DeleteAsync(id);

            return NoContent();
        }
    }
}
