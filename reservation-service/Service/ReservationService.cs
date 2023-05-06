using reservation_service.Model;
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

        public async Task CreateAsync(Reservation newReservation) =>
            await _repository.CreateAsync(newReservation);

        public async Task UpdateAsync(Guid id, Reservation updateReservation) =>
            await _repository.UpdateAsync(id, updateReservation);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);

    }
}