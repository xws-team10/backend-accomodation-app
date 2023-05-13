using search_service.DTO;

namespace search_service.ProtoServices
{
    public interface IFreeAccomodations
    {
        IEnumerable<AccomodationDto> GetAllFreeAccomodations(DateTime startDate, DateTime endDate);
    }
}
