using search_service.DTO;
using search_service.Model;
using search_service.ProtoServices;
using search_service.Repository;
using search_service.Service.Core;

namespace search_service.Service
{
    public class AccomodationSearchService : IAccomodationSearchService
    {
        private readonly AccomodationRepository _repository;
        private readonly FreeAccomodations freeAccomodations;

        public AccomodationSearchService(AccomodationRepository repository, FreeAccomodations freeAccomodations)
        {
            _repository = repository;
            this.freeAccomodations = freeAccomodations;
        }
        public async Task<List<Accomodation>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<List<Accomodation>> GetBySearch(int capacity, DateTime startDate, DateTime endDate, string place, int price)
        {

            List<Accomodation> AccomodationsBySearch = new List<Accomodation>();
            List<AccomodationDto> freeDate = (List<AccomodationDto>)freeAccomodations.GetAllFreeAccomodations(startDate, endDate).Distinct().ToList();

            foreach (Accomodation accomodation in await GetAllAsync())
            { 
                if(accomodation.Price <= price && accomodation.Capacity >= capacity)
                if(((accomodation.Address.City).ToLower()).Equals(place.ToLower()) || place == "")
                if(isFree(freeDate,accomodation.Id))
                    AccomodationsBySearch.Add(accomodation);
            }
            return AccomodationsBySearch;
        }
        public bool isFree(List<AccomodationDto> freeIds, Guid id)
        {
            foreach(AccomodationDto accomodation in freeIds) {
                if(accomodation.Id.Equals(id)) 
                    return false;
            }
            return true;
        }

        public async Task CreateAsync(Accomodation newAccomodation)
        {
            List<Accomodation> reservations = await GetAllAsync();
            List<Accomodation> filteredReservations = reservations.FindAll(r => r.Id.Equals(newAccomodation.Id));

            await _repository.CreateAsync(newAccomodation);
        }
    }
}
