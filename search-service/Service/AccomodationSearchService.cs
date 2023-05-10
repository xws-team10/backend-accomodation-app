using search_service.Model;
using search_service.Repository;
using search_service.Service.Core;

namespace search_service.Service
{
    public class AccomodationSearchService : IAccomodationSearchService
    {
        private readonly AccomodationRepository _repository;

        public AccomodationSearchService(AccomodationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Accomodation>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<List<Accomodation>> GetBySearch(int capacity, DateTime date, string place, int price)
        {
            List<Accomodation> AccomodationsBySearch = new List<Accomodation>();
            foreach (Accomodation accomodation in await GetAllAsync())
            { 
                if(accomodation.Price <= price && accomodation.Capacity >= capacity)
                if(((accomodation.Address.City).ToLower()).Equals(place.ToLower()) || place == "")
                    AccomodationsBySearch.Add(accomodation);
            }
            return AccomodationsBySearch;
        }

        public async Task CreateAsync(Accomodation newAccomodation)
        {
            List<Accomodation> reservations = await GetAllAsync();
            List<Accomodation> filteredReservations = reservations.FindAll(r => r.Id.Equals(newAccomodation.Id));

            await _repository.CreateAsync(newAccomodation);
        }
    }
}
