﻿using reservation_service.Model;
using reservation_service.Repository;
using reservation_service.Service.Core;

namespace reservation_service.Service
{
    public class ReservationService : IReservationService
    {
        private readonly ReservationRepository _repository;

        public ReservationService(ReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Reservation>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Reservation> GetByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<List<Reservation>> GetAllByGuestUsernameAsync(string username) =>
            await _repository.GetAllByGuestUsernameAsync(username);

        public async Task<List<Reservation>> GetAllByAccomodationIdAsync(Guid id) =>
            await _repository.GetAllByAccomodationIdAsync(id);

        public async Task CreateAsync(Reservation newReservation)
        {
            List<Reservation> reservations = await GetAllAsync();
            List<Reservation> filteredReservations = reservations.FindAll(r => r.AccomodationId.Equals(newReservation.AccomodationId));

            foreach (Reservation reservation in filteredReservations)
            {
                if (reservation.Overlaps(newReservation))
                    return;
            }

            await _repository.CreateAsync(newReservation);
        }

        public async Task UpdateAsync(Guid id, Reservation updateReservation) =>
            await _repository.UpdateAsync(id, updateReservation);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);

    }
}