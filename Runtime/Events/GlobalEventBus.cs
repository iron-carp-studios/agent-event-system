using IronCarpStudios.AES.Agents;
using System;

namespace IronCarpStudios.AES.Events
{
    public class GlobalEvent
    {
        private static EventBus eventBus;
        static GlobalEvent()
        {
            eventBus = new EventBus();
        }

        public static void AddListener(EventRegistrationData eventData)
        {
            eventBus.RegisterEvent(eventData);
        }

        internal static void AddListener(string v, object onUpdatePlayerDynamicHud)
        {
            throw new NotImplementedException();
        }

        public static void Broadcast(string eventName, Agent sender, AgentEventArgs args)
        {
            eventBus.BroadcastEvent(eventName, sender, args);
        }

        public static void RemoveListener(EventRegistrationData eventData)
        {
            eventBus.TryRemoveEvent(eventData);
        }
    }
}
