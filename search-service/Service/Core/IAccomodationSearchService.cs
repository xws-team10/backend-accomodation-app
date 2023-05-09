using search_service.Model;

namespace search_service.Service.Core
{
    public interface IAccomodationSearchService
    {
        Task<List<Accomodation>> GetAllAsync();
        Task<List<Accomodation>> GetBySearch(int capacity, DateTime date, string place, int price);
        Task CreateAsync(Accomodation newAccomodation);
    }
}
