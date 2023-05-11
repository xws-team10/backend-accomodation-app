using accomodation_service.Model;

namespace accomodation_service.Repository.Core
{
    public interface IAccomodationRepository
    {
        Task<IEnumerable<Accomodation>> GetAllAsync();
        Task CreateAsync(Accomodation accomodation);

        Task<Accomodation> GetAccomodationById(Guid id);
    }
}
