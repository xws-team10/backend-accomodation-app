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
        private readonly ReservationService _reservationService;

        public ReservationRequestController(ReservationRequestService reservationRequestService, ReservationService reservationService)
        {
            _reservationRequestService = reservationRequestService;
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<List<ReservationRequest>> Get() =>
           await _reservationRequestService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationRequest>> Get(Guid id)
        {
            var reservationRequest = await _reservationRequestService.GetByIdAsync(id);

            if (reservationRequest is null)
                return NotFound();

            return reservationRequest;
        }

        [HttpGet("getByGuest/{id}")]
        public async Task<List<ReservationRequest>> GetByGuestUsername(string id) =>
           await _reservationRequestService.GetAllByGuestUsernameAsync(id);

        [HttpGet("getByAccomodation/{id}")]
        public async Task<List<ReservationRequest>> GetByAccomodationId(Guid id) =>
           await _reservationRequestService.GetAllByAccomodationIdAsync(id);

        [HttpPost]
        public async Task<IActionResult> Post(ReservationRequest newReservationRequest)
        {
            if (!newReservationRequest.Validate())
                return BadRequest();

            if (!IsAvailable(newReservationRequest).Result)
                return BadRequest();

            await _reservationRequestService.CreateAsync(newReservationRequest);

            return CreatedAtAction(nameof(Get), new { id = newReservationRequest.Id }, newReservationRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ReservationRequest updateReservationRequest)
        {
            if (!updateReservationRequest.Validate())
                return BadRequest();

            var reservationRequest = await _reservationRequestService.GetByIdAsync(id);

            if (reservationRequest is null)
                return NotFound();

            updateReservationRequest.Id = reservationRequest.Id;

            if (updateReservationRequest.Status.Equals(Status.APPROVED))
            {
                Reservation newReservation = new()
                {
                    StartDate = updateReservationRequest.StartDate,
                    EndDate = updateReservationRequest.EndDate,
                    NumberOfGuests = updateReservationRequest.NumberOfGuests,
                    AccomodationId = updateReservationRequest.AccomodationId,
                    GuestUsername = updateReservationRequest.GuestUsername
                };
                if (IsAvailable(newReservation).Result)
                    await _reservationService.CreateAsync(newReservation);
                else
                    return BadRequest();
            }

            await _reservationRequestService.UpdateAsync(id, updateReservationRequest);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reservationRequest = await _reservationRequestService.GetByIdAsync(id);

            if (reservationRequest is null)
                return NotFound();

            if(!reservationRequest.Status.Equals(Status.PENDING))
                return BadRequest();

            await _reservationRequestService.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> IsAvailable(ReservationRequest reservationRequest)
        {
            List<Reservation> reservations = await _reservationService.GetAllAsync();
            List<Reservation> filteredReservations = reservations.FindAll(r => r.AccomodationId.Equals(reservationRequest.AccomodationId));

            foreach (Reservation reservation in filteredReservations)
            {
                if (reservation.Overlaps(reservationRequest))
                    return false;
            }
            return true;
        }

        private async Task<bool> IsAvailable(Reservation reservation)
        {
            List<Reservation> reservations = await _reservationService.GetAllAsync();
            List<Reservation> filteredReservations = reservations.FindAll(r => r.AccomodationId.Equals(reservation.AccomodationId));

            foreach (Reservation res in filteredReservations)
            {
                if (res.Overlaps(reservation))
                    return false;
            }
            return true;
        }
    }
}
