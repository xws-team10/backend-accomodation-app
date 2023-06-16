using accomodation_service;

namespace reservation_service.ProtoServices
{
    public interface IGetAccomodationHost
    {
        AccomodationHostResponse GetHost(Guid id);
    }
}
