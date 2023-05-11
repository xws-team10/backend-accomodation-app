using accomodation_service.Model;

namespace accomodation_service.Service.Core
{
    public interface IAccomodationService
    {
        Task<IEnumerable<Accomodation>> GetAllAsync();
        Task CreateAsync(Accomodation newAccomodation);
        Task<Accomodation> GetAccomodationById(Guid id);
    }
}