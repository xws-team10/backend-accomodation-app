using accomodation_service.Model;

namespace accomodation_service.Service.Core
{
    public interface IAccomodationService
    {
        Task<List<Accomodation>> GetAllAsync();
        Task CreateAsync(Accomodation newAccomodation);
    }
}