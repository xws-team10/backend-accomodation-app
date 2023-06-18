using host_service;

namespace reservation_service.ProtoServices
{
    public interface IGetAccomodationByHostServiceClient
    {
        AccomodationsResponse GetAccommodationsByHostId(string hostId);
    }
}
