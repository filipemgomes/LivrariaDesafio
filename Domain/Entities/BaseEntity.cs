using System.Collections.Generic;
using FluentValidation.Results;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        private readonly List<string> _notifications = new List<string>();
        public IReadOnlyCollection<string> Notifications => _notifications;

        public ValidationResult ValidationResult { get; protected set; }

        public void AddNotification(string notification) => _notifications.Add(notification);

        public bool HasNotifications() => _notifications.Count > 0;

        public abstract bool Validate();
    }
}
