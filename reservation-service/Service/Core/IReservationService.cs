﻿using reservation_service.Model;

namespace reservation_service.Service.Core
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(Guid id);
        Task<List<Reservation>> GetAllByGuestUsernameAsync(string username);
        Task<List<Reservation>> GetAllByAccomodationIdAsync(Guid id);
        Task CreateAsync(Reservation newReservation);
        Task UpdateAsync(Guid id, Reservation updateReservation);
        Task DeleteAsync(Guid id);
    }
}
