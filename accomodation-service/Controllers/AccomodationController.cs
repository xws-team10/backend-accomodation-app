using Microsoft.AspNetCore.Mvc;
using accomodation_service.Model;
using accomodation_service.Service;

namespace accomodation_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccomodationController : ControllerBase
    {
        private readonly AccomodationService _service;
        public AccomodationController(AccomodationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Accomodation>> Get() =>
            await _service.GetAllAsync();

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Accomodation newAccomodation)
        {
            await _service.CreateAsync(newAccomodation);
            return CreatedAtAction(nameof(Get), new { id = newAccomodation.Id }, newAccomodation);
        }
    }

}