using Microsoft.AspNetCore.Mvc;
using accomodation_service.Model;
using accomodation_service.Service;
using accomodation_service.ProtoServices;

namespace accomodation_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccomodationGradeController : ControllerBase
    {
        private readonly AccomodationGradeService _accomodationGradeService;
        private readonly AccomodationService _accomodationService;
        private readonly SendNotification _sendNotification;

        public AccomodationGradeController(
            AccomodationGradeService accomodationGradeService,
            AccomodationService accomodationService,
            SendNotification sendNotification)
        {
            _accomodationGradeService = accomodationGradeService;
            _accomodationService = accomodationService;
            _sendNotification = sendNotification;
        }

        [HttpGet]
        public async Task<List<AccomodationGrade>> Get() =>
           await _accomodationGradeService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<AccomodationGrade>> Get(Guid id)
        {
            var accomodation = await _accomodationGradeService.GetByIdAsync(id);

            if (accomodation is null)
                return NotFound();

            return accomodation;
        }

        [HttpGet("getByGuest/{id}")]
        public async Task<List<AccomodationGrade>> GetByGuestUsername(string id) =>
           await _accomodationGradeService.GetAllByGuestUsernameAsync(id);

        [HttpGet("getByAccomodation/{id}")]
        public async Task<List<AccomodationGrade>> GetByAccomodationId(Guid id) =>
           await _accomodationGradeService.GetAllByAccomodationIdAsync(id);

        [HttpGet("getByGuestAndAccomodation/{username}/{id}")]
        public async Task<List<AccomodationGrade>> GetByGuestAndAccomodation(string username, Guid id) =>
           await _accomodationGradeService.GetAllByGuestAndAccomodationAsync(username, id);

        [HttpPost]
        public async Task<IActionResult> Post(AccomodationGrade newAccomodationGrade)
        {
            if (!newAccomodationGrade.Validate())
                return BadRequest();

            await _accomodationGradeService.CreateAsync(newAccomodationGrade);

            Accomodation accomodation = await _accomodationService.GetAccomodationById(newAccomodationGrade.AccomodationId);
            _sendNotification.CreateNotification("A guest has graded your accomodation " + accomodation.Name, accomodation.HostId, 3);
            
            return CreatedAtAction(nameof(Get), new { id = newAccomodationGrade.Id }, newAccomodationGrade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, AccomodationGrade updateAccomodationGrade)
        {
            if (!updateAccomodationGrade.Validate())
                return BadRequest();

            var accomodationGrade = await _accomodationGradeService.GetByIdAsync(id);

            if (accomodationGrade is null)
                return NotFound();

            updateAccomodationGrade.Id = accomodationGrade.Id;

            await _accomodationGradeService.UpdateAsync(id, updateAccomodationGrade);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var accomodationGrade = await _accomodationGradeService.GetByIdAsync(id);

            if (accomodationGrade is null)
                return NotFound();

            await _accomodationGradeService.DeleteAsync(id);

            return NoContent();
        }
    }
}
