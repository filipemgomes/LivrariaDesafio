namespace Domain.Notifications
{
    public class DomainNotification
    {
        public string Key { get; private set; }
        public string Message { get; private set; }

        public DomainNotification(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}
