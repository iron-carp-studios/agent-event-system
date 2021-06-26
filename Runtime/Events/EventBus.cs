using System;
using System.Collections.Generic;

namespace IronCarp.AES.Events
{
    public class EventBus
    {
        private readonly Dictionary<string, Action<EventParameter>> eventList;

        public EventBus()
        {
            eventList = new Dictionary<string, Action<EventParameter>>();
        }

        public void RegisterEvent(EventRegistrationData eventData)
        {
            var key = eventData.EventType.ToLower();

            if (eventList.ContainsKey(key))
            {
                eventList[key] += eventData.Action;
            }
            else
            {
                eventList.Add(key, eventData.Action);
            }
        }

        public bool TryRemoveEvent(EventRegistrationData eventData)
        {
            Action<EventParameter> action;
            if (eventList.TryGetValue(eventData.EventType, out action))
            {
                if(action != null)
                {
                    eventList[eventData.EventType] -= eventData.Action;
                    return true;
                }
            }

            return false;
        }

        public void BroadcastEvent(string eventType, EventParameter parameters)
        {
            Action<EventParameter> action;
            if (eventList.TryGetValue(eventType.ToLower(), out action))
            {
                action.Invoke(parameters);
            }
        }
    }
}
