using reservation_service.Model;
using reservation_service.Repository;
using reservation_service.Service.Core;

namespace reservation_service.Service
{
    public class ReservationRequestService : IReservationRequestService
    {
        private readonly ReservationRequestRepository _repository;

        public ReservationRequestService(ReservationRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ReservationRequest>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<ReservationRequest> GetByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);
        public async Task<List<ReservationRequest>> GetAllByGuestIdAsync(Guid id) =>
            await _repository.GetAllByGuestIdAsync(id);

        public async Task<List<ReservationRequest>> GetAllByAccomodationIdAsync(Guid id) =>
            await _repository.GetAllByAccomodationIdAsync(id);

        public async Task CreateAsync(ReservationRequest newReservationRequest) =>
            await _repository.CreateAsync(newReservationRequest);

        public async Task UpdateAsync(Guid id, ReservationRequest updateReservationRequest) =>
            await _repository.UpdateAsync(id, updateReservationRequest);

        public async Task DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);
    }
}
