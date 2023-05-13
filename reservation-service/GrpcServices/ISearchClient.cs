using reservation_service.Model;

namespace reservation_service.GrpcServices
{
    public interface ISearchClient
    {
        IEnumerable<ReservationRequest> ReturnAllAccomodations(Guid id);
    }
}
