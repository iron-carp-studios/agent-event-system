using IronCarpStudios.AES.Agents;
using System;

namespace IronCarpStudios.AES.Events
{
    public class EventRegistrationData
    {
        public readonly string EventType;
        public Action<Agent, AgentEventArgs> Action;
        public EventQueuePriority Priority;

        public EventRegistrationData(string eventType, Action<Agent, AgentEventArgs> action, EventQueuePriority priority = EventQueuePriority.Normal)
        {
            EventType = eventType;
            Action = action;
            Priority = priority;
        }
    }
}