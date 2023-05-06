using Microsoft.AspNetCore.Mvc;
using reservation_service.Model;
using reservation_service.Service;

namespace reservation_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<List<Reservation>> Get() =>
           await _reservationService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> Get(Guid id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            return reservation;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Reservation newReservation)
        {
            await _reservationService.CreateAsync(newReservation);

            return CreatedAtAction(nameof(Get), new { id = newReservation.Id }, newReservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Reservation updateReservation)
        {
            var reservation = await _reservationService.GetByIdAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }
            updateReservation.Id = reservation.Id;

            await _reservationService.UpdateAsync(id, updateReservation);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);

            if (reservation is null)
            {
                return NotFound();
            }

            await _reservationService.DeleteAsync(id);

            return NoContent();
        }
    }
}
