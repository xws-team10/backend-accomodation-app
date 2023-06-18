using accomodation_service.Dtos;
using accomodation_service.Model;

namespace accomodation_service.Repository.Core
{
    public interface IAccomodationRepository
    {
        Task<IEnumerable<Accomodation>> GetAllAsync();
        Task CreateAsync(Accomodation accomodation);

        Task<Accomodation> GetAccomodationById(Guid id);

        Task<bool> AccomodationUpdate(AccomodationChangeDto accomodationChangeDto);
        Task AccomodationChangePrice(AccomodationChangePriceDto accomodationChangePriceDto);
    }
}
