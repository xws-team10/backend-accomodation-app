using accomodation_service.Dtos;
using System;

namespace accomodation_service.ProtoServices
{
    public interface ICreateAccomodation
    {
        bool CreateNewAccomodation(Guid Id, Guid HostId, string Name, string Description, int Price, int Capacity, string Country, string City, string Street, string StreetNumber, DateTime AvailableFromDate, DateTime AvailableToDate);
        bool UpdateAccomodation(Guid Id, DateTime AvailableFromDate, DateTime AvailableToDate);

        void UpdateAccomodationPrice(AccomodationChangePriceDto changePriceDto);

        bool CheckReservations(Guid id, DateTime startDate, DateTime endDate);

        
    }
}
