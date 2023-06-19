using search_service.DTO;
using search_service.Model;
using search_service.ProtoServices;
using search_service.Repository;
using search_service.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<List<Accomodation>> GetBySearch(int capacity, DateTime startDate, DateTime endDate, string place, int price, string amenities, string host)
        {
            List<Accomodation> accomodationsBySearch = new List<Accomodation>();
            List<AccomodationDto> freeDate = (List<AccomodationDto>)freeAccomodations.GetAllFreeAccomodations(startDate, endDate).Distinct().ToList();

            foreach (Accomodation accomodation in await GetAllAsync())
            {
                    if (capacity == 0 || accomodation.Capacity >= capacity) 
                    if (price == 0 || accomodation.Price <= price) 
                    if(string.IsNullOrEmpty(place) || accomodation.Address.City.ToLower() == place.ToLower()) 
                    if (isFree(freeDate, accomodation.Id) )
                    if(startDate == DateTime.MinValue || accomodation.AvailableFromDate <= startDate) 
                    if(endDate == DateTime.MinValue || accomodation.AvailableToDate >= endDate) 
                    if(string.IsNullOrEmpty(amenities) || accomodation.Description.Contains(amenities)) 
                    if(string.IsNullOrEmpty(host) || accomodation.Description == host)
                {
                    accomodationsBySearch.Add(accomodation);
                }
            }

            return accomodationsBySearch;
        }

        public bool isFree(List<AccomodationDto> freeIds, Guid id)
        {
            foreach (AccomodationDto accomodation in freeIds)
            {
                if (accomodation.Id.Equals(id))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task CreateAsync(Accomodation newAccomodation)
        {
            List<Accomodation> reservations = await GetAllAsync();
            List<Accomodation> filteredReservations = reservations.FindAll(r => r.Id.Equals(newAccomodation.Id));

            await _repository.CreateAsync(newAccomodation);
        }

        public async Task AccomodationUpdate(AccomodationUpdateDto accomodationChangeDto)
        {
            await _repository.AccomodationUpdate(accomodationChangeDto);
        }

        public Task<List<Accomodation>> GetBySearch(int capacity, DateTime startDate, DateTime endDate, string place, int price)
        {
            throw new NotImplementedException();
        }
    }
}
