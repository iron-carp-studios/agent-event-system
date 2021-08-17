using IronCarpStudios.AES.Agents;
using System;
using System.Collections.Generic;

namespace IronCarpStudios.AES.Events
{
    public class EventBus
    {
        private readonly Dictionary<string, Action<Agent, AgentEventArgs>> eventList;

        public EventBus()
        {
            eventList = new Dictionary<string, Action<Agent, AgentEventArgs>>();
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
            Action<Agent, AgentEventArgs> action;
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

        public void BroadcastEvent(string eventType, Agent sender, AgentEventArgs parameters)
        {
            Action<Agent, AgentEventArgs> action;
            if (eventList.TryGetValue(eventType.ToLower(), out action))
            {
                action.Invoke(sender, parameters);
            }
        }
    }
}
