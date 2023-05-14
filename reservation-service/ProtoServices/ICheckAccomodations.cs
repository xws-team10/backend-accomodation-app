namespace reservation_service.ProtoServices
{
    public interface ICheckAccomodations
    {
        bool CheckAccomodadtions(Guid id, DateTime startDate, DateTime endDate);
    }
}
