using accomodation_service.Model;

namespace accomodation_service.Repository.Core
{
    public interface IAccomodationRepository
    {
        Task<List<Accomodation>> GetAllAsync();
        Task CreateAsync(Accomodation accomodation);
    }
}
