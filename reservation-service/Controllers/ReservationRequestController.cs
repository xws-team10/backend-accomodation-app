using Microsoft.AspNetCore.Mvc;
using reservation_service.Model;
using reservation_service.Service;

namespace reservation_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationRequestController : ControllerBase
    {
        private readonly ReservationRequestService _reservationRequestService;

        public ReservationRequestController(ReservationRequestService reservationRequestService)
        {
            _reservationRequestService = reservationRequestService;
        }

        [HttpGet]
        public async Task<List<ReservationRequest>> Get() =>
           await _reservationRequestService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationRequest>> Get(Guid id)
        {
            var reservationRequest = await _reservationRequestService.GetByIdAsync(id);

            if (reservationRequest is null)
            {
                return NotFound();
            }

            return reservationRequest;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ReservationRequest newReservationRequest)
        {
            await _reservationRequestService.CreateAsync(newReservationRequest);

            return CreatedAtAction(nameof(Get), new { id = newReservationRequest.Id }, newReservationRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ReservationRequest updateReservationRequest)
        {
            var reservationRequest = await _reservationRequestService.GetByIdAsync(id);

            if (reservationRequest is null)
            {
                return NotFound();
            }
            updateReservationRequest.Id = reservationRequest.Id;

            await _reservationRequestService.UpdateAsync(id, updateReservationRequest);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reservationRequest = await _reservationRequestService.GetByIdAsync(id);

            if (reservationRequest is null)
            {
                return NotFound();
            }

            await _reservationRequestService.DeleteAsync(id);

            return NoContent();
        }
    }
}
