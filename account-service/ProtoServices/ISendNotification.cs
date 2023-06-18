namespace account_service.ProtoServices
{
    public interface ISendNotification
    {
        bool CreateNotification(string text, Guid userId, int type);
    }
}
