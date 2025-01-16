using System.Collections.Generic;
using System.Linq;

namespace Domain.Notifications
{
    public class DomainNotificationHandler
    {
        private readonly List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public void AddNotification(DomainNotification notification)
        {
            _notifications.Add(notification);
        }

        public IReadOnlyCollection<DomainNotification> GetNotifications()
        {
            return _notifications.AsReadOnly();
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void ClearNotifications()
        {
            _notifications.Clear();
        }
    }
}
