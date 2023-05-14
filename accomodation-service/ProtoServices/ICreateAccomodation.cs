using accomodation_service.Dtos;
using System;

namespace accomodation_service.ProtoServices
{
    public interface ICreateAccomodation
    {
        void CreateNewAccomodation(Guid Id, string Name, string Description, int Price, int Capacity, string Country, string City, string Street, string StreetNumber, DateTime AvailableFromDate, DateTime AvailableToDate);
        void UpdateAccomodation(Guid Id, DateTime AvailableFromDate, DateTime AvailableToDate);

        void UpdateAccomodationPrice(AccomodationChangePriceDto changePriceDto);

        bool CheckReservations(Guid id, DateTime startDate, DateTime endDate);
    }
}
