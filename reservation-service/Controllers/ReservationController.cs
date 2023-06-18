﻿using Microsoft.AspNetCore.Mvc;
using reservation_service.Model;
using reservation_service.ProtoServices;
using reservation_service.Service;

namespace reservation_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;
        private readonly CheckAccomodations _checkAccomodations;

        public ReservationController(ReservationService reservationService, CheckAccomodations checkAccomodations)
        {
            _reservationService = reservationService;
            _checkAccomodations = checkAccomodations;
        }

        [HttpGet]
        public async Task<List<Reservation>> Get() =>
           await _reservationService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> Get(Guid id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);

            if (reservation is null)
                return NotFound();

            return reservation;
        }

        [HttpGet("getByGuest/{id}")]
        public async Task<List<Reservation>> GetByGuestUsername(string id) =>
           await _reservationService.GetAllByGuestUsernameAsync(id);

        [HttpGet("getByAccomodation/{id}")]
        public async Task<List<Reservation>> GetByAccomodationId(Guid id) =>
           await _reservationService.GetAllByAccomodationIdAsync(id);

        [HttpPost]
        public async Task<IActionResult> Post(Reservation newReservation)
        {
            if (!newReservation.Validate())
                return BadRequest();

            if (!IsAvailable(newReservation).Result)
                return BadRequest();
            
           if (!_checkAccomodations.CheckAccomodadtions(newReservation.AccomodationId, newReservation.StartDate, newReservation.EndDate))
                return BadRequest();

            await _reservationService.CreateAsync(newReservation);

            return CreatedAtAction(nameof(Get), new { id = newReservation.Id }, newReservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Reservation updateReservation)
        {
            if (!updateReservation.Validate())
                return BadRequest();

            var reservation = await _reservationService.GetByIdAsync(id);

            if (reservation is null)
                return NotFound();

            updateReservation.Id = reservation.Id;

            await _reservationService.UpdateAsync(id, updateReservation);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);

            if (reservation is null)
                return NotFound();

            if (reservation.StartDate <= DateTime.Now.AddDays(1))
                return BadRequest();

            await _reservationService.DeleteAsync(id);

            return NoContent();
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

        [HttpGet("canGuestGradeAccomodation/{username}/{id}")]
        public async Task<bool> CanGuestGradeAcccomodation(string username, Guid id) =>
            await _reservationService.CanGuestGradeAccomodation(username, id);

        [HttpGet("isSuperHost/{hostId}")]
        public async Task<bool> isSuperHost(string hostId)
        {
            string hostIdString = hostId.ToString();

            double cancellationRate = await _reservationService.GetCancellationRateByHostAsync(hostIdString);
            int reservationCount = await _reservationService.GetReservationCountByHostIdAsync(hostIdString);
            int totalBookedDays =  await _reservationService.GetTotalReservedDaysByHostId(hostIdString);

            if (cancellationRate > 5 && reservationCount > 5 && totalBookedDays > 50)
            {
                return true;
            }

            return false;
        }

    }
}
