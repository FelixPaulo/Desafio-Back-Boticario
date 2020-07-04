using Cashbot.Application.Events;
using System;

namespace Cashbot.Application.Notifications
{
    public class ApplicationNotification : Event
    {
        public Guid NotificationId { get; private set; }
        //public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public ApplicationNotification(string value)
        {
            NotificationId = Guid.NewGuid();
            //Key = key;
            Value = value;
            Version = 1;
        }
    }
}
