using accomodation_service.Dtos;
using accomodation_service.Model;

namespace accomodation_service.Service.Core
{
    public interface IAccomodationService
    {
        Task<IEnumerable<Accomodation>> GetAllAsync();
        Task CreateAsync(Accomodation newAccomodation);
        Task<Accomodation> GetAccomodationById(Guid id);
        Task AccomodationUpdate(AccomodationChangeDto accomodationChangeDto);
        Task<bool> AvailabilityCheck(Guid id, DateTime from, DateTime to);
        Task AccomodationChangePrice(AccomodationChangePriceDto accomodationChangePriceDto);
    }
}