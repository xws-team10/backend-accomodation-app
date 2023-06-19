using search_service.DTO;
using search_service.Model;

namespace search_service.Service.Core
{
    public interface IAccomodationSearchService
    {
        Task<List<Accomodation>> GetAllAsync();
        Task CreateAsync(Accomodation newAccomodation);
        Task AccomodationUpdate(AccomodationUpdateDto accomodationChangeDto);
        Task<List<Accomodation>> GetBySearch(int capacity, DateTime startDate, DateTime endDate, string place, int price, string amenities, string host);
    }
}
