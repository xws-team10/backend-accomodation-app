using accomodation_service.Model;
using accomodation_service.Repository;
using accomodation_service.Service.Core;


namespace accomodation_service.Service
{
    public class AccomodationService : IAccomodationService
    {
        private readonly AccomodationRepository _repository;

        public AccomodationService(AccomodationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Accomodation>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task CreateAsync(Accomodation newAccomodation)
        {
            await _repository.CreateAsync(newAccomodation);
        }
    }
}