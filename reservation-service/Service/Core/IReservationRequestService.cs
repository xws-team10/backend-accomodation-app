using reservation_service.Model;

namespace reservation_service.Service.Core
{
    public interface IReservationRequestService
    {
        Task<List<ReservationRequest>> GetAllAsync();
        Task<ReservationRequest> GetByIdAsync(Guid id);
        Task CreateAsync(ReservationRequest newReservationRequest);
        Task UpdateAsync(Guid id, ReservationRequest updateReservationRequest);
        Task DeleteAsync(Guid id);
    }
}
