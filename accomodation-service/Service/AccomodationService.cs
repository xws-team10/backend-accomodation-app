using accomodation_service.Dtos;
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

        public async Task<IEnumerable<Accomodation>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task CreateAsync(Accomodation newAccomodation)
        {
            await _repository.CreateAsync(newAccomodation);
        }

        public async Task<Accomodation> GetAccomodationById(Guid id) =>
            await _repository.GetAccomodationById(id);

        public async Task AccomodationUpdate(AccomodationChangeDto accomodationChangeDto)
        {
            await _repository.AccomodationUpdate(accomodationChangeDto);
        }
    }
}