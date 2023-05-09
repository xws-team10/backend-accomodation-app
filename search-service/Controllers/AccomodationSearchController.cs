using Microsoft.AspNetCore.Mvc;
using search_service.Model;
using search_service.Service;

namespace search_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccomodationSearchController : ControllerBase
    {
        private readonly AccomodationSearchService _service;

        public AccomodationSearchController(AccomodationSearchService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<List<Accomodation>> Get() =>
            await _service.GetAllAsync();

        [HttpPost]
        public async Task<IActionResult> Post(Accomodation newAccomodation)
        {
            await _service.CreateAsync(newAccomodation);
            return CreatedAtAction(nameof(Get), new { id = newAccomodation.Id }, newAccomodation);
        }

        [HttpGet("getBySearch")]
        public async Task<List<Accomodation>> GetBySearch(DateTime date, int capacity = 0, int price = 0, string place = "") =>
            await _service.GetBySearch(capacity,date,place,price);
    }
}
