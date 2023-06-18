using Microsoft.AspNetCore.Mvc;
using notification_service.Model;
using notification_service.Service;

namespace notification_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationUserSettingsController : ControllerBase
    {
        private readonly NotificationUserSettingsService _notificationUserSettingsService;

        public NotificationUserSettingsController(NotificationUserSettingsService notificationUserSettingsService)
        {
            _notificationUserSettingsService = notificationUserSettingsService;
        }

        [HttpGet]
        public async Task<List<NotificationUserSettings>> Get() =>
           await _notificationUserSettingsService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationUserSettings>> Get(Guid id)
        {
            var notificationUserSettings = await _notificationUserSettingsService.GetByIdAsync(id);

            if (notificationUserSettings is null)
                return NotFound();

            return notificationUserSettings;
        }

        [HttpGet("getByUser/{id}")]
        public async Task<NotificationUserSettings> GetByUser(Guid id) =>
           await _notificationUserSettingsService.GetByUserAsync(id);

        [HttpPost]
        public async Task<IActionResult> Post(NotificationUserSettings newNotificationUserSettings)
        {
            await _notificationUserSettingsService.CreateAsync(newNotificationUserSettings);

            return CreatedAtAction(nameof(Get), new { id = newNotificationUserSettings.Id }, newNotificationUserSettings);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, NotificationUserSettings updateNotificationUserSettings)
        {
            var notificationUserSettings = await _notificationUserSettingsService.GetByIdAsync(id);

            if (notificationUserSettings is null)
                return NotFound();

            updateNotificationUserSettings.Id = notificationUserSettings.Id;

            await _notificationUserSettingsService.UpdateAsync(id, updateNotificationUserSettings);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var notificationUserSettings = await _notificationUserSettingsService.GetByIdAsync(id);

            if (notificationUserSettings is null)
                return NotFound();

            await _notificationUserSettingsService.DeleteAsync(id);

            return NoContent();
        }
    }
}
