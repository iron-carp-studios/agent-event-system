using System;

namespace IronCarp.AES.Events
{
    public class EventRegistrationData
    {
        public readonly string EventType;
        public Action<EventParameter> Action;
        public EventQueuePriority Priority;

        public EventRegistrationData(string eventType, Action<EventParameter> action, EventQueuePriority priority = EventQueuePriority.Normal)
        {
            EventType = eventType;
            Action = action;
            Priority = priority;
        }
    }
}