using search_service.DTO;
using search_service.Model;

namespace search_service.Repository.Core
{
    public interface IAccomodationRepository
    {
        Task<List<Accomodation>> GetAllAsync();
        Task CreateAsync(Accomodation accomodation);
        Task AccomodationUpdate(AccomodationUpdateDto accomodationChangeDto);
    }
}
