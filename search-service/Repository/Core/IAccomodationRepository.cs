using search_service.DTO;
using search_service.Model;

namespace search_service.Repository.Core
{
    public interface IAccomodationRepository
    {
        Task<List<Accomodation>> GetAllAsync();
        Task CreateAsync(Accomodation accomodation);
        Task<Accomodation> GetAccomodationById(Guid id);
        Task AccomodationUpdate(AccomodationUpdateDto accomodationChangeDto);
        Task AccomodationChangePrice(AccomodationChangePriceDto accomodationChangePriceDto);
    }
}
