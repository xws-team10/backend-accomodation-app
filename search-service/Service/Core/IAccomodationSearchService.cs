﻿using search_service.Model;

namespace search_service.Service.Core
{
    public interface IAccomodationSearchService
    {
        Task<List<Accomodation>> GetAllAsync();
        Task<List<Accomodation>> GetBySearch(int capacity, DateTime startDate, DateTime endDate, string place, int price);
        Task CreateAsync(Accomodation newAccomodation);
    }
}
