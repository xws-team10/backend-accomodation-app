using account_service.Model;
using account_service.ProtoServices;
using account_service.Service;
using Microsoft.AspNetCore.Mvc;

namespace account_service.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HostGradeController : ControllerBase
    {
        private readonly HostGradeService _hostGradeService;
        private readonly SendNotification _sendNotification;

        public HostGradeController(HostGradeService hostGradeService, SendNotification sendNotification)
        {
            _hostGradeService = hostGradeService;
            _sendNotification = sendNotification;
        }

        [HttpGet]
        public async Task<List<HostGrade>> Get() =>
            await _hostGradeService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<HostGrade>> Get(Guid id)
        {
            var hostGrade = await _hostGradeService.GetGradeByIdAsync(id);

            if(hostGrade is null)
                return NotFound();

            return hostGrade;
        }

        [HttpGet("getByGuest/{id}")]
        public async Task<List<HostGrade>> GetByGuestUsernamem(string id) =>
            await _hostGradeService.GetAllByGuestUsernameAsync(id);

        [HttpGet("getByHost/{id}")]
        public async Task<List<HostGrade>> GetByHostId(Guid id) =>
           await _hostGradeService.GetAllByHostIdAsync(id);

        [HttpGet("getByGuestAndHost/{username}/{id}")] 
        public async Task<List<HostGrade>> GetByGuestAndAccomodation(string username, Guid id) =>
           await _hostGradeService.GetAllByGuestAndHostAsync(username, id);

        [HttpPost]
        public async Task<IActionResult> Create(HostGrade newHostGrade)
        {
            if(!newHostGrade.Validate())
                return BadRequest();

            await _hostGradeService.CreateAsync(newHostGrade);
            _sendNotification.CreateNotification("A guest has graded you.", newHostGrade.HostId, 2);
            return CreatedAtAction(nameof(Get), new { id = newHostGrade.Id }, newHostGrade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, HostGrade updateHostGrade)
        {
            if (!updateHostGrade.Validate())
                return BadRequest();

            var hostGrade = await _hostGradeService.GetGradeByIdAsync(id);

            if (hostGrade is null)
                return NotFound();

            updateHostGrade.Id = hostGrade.Id;

            await _hostGradeService.UpdateAsync(id, updateHostGrade);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var hostGrade = await _hostGradeService.GetGradeByIdAsync(id);

            if (hostGrade is null)
                return NotFound();

            await _hostGradeService.DeleteAsync(id);

            return NoContent();
        }
    }
}
