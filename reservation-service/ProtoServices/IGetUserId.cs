namespace reservation_service.ProtoServices
{
    public interface IGetUserId
    {
        Guid? GetUserByUsername(string username);
    }
}
