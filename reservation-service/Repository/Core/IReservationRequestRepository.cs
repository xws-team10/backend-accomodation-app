using reservation_service.Model;

namespace reservation_service.Repository.Core
{
    public interface IReservationRequestRepository
    {
        Task<List<ReservationRequest>> GetAllAsync();
        Task<ReservationRequest> GetByIdAsync(Guid id);
        Task<List<ReservationRequest>> GetAllByGuestIdAsync(Guid id);
        Task<List<ReservationRequest>> GetAllByAccomodationIdAsync(Guid id);
        Task CreateAsync(ReservationRequest newReservationRequest);
        Task UpdateAsync(Guid id, ReservationRequest updateReservationRequest);
        Task DeleteAsync(Guid id);
    }
}
