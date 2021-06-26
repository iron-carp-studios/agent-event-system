using IronCarp.AES.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IronCarp.AES.Agents
{
    public class Agent : MonoBehaviour
    {
        public Guid AgentId;
        private List<AgentComponent> Components;
        private List<EventBus> events;

        private void Awake()
        {
            AgentId = new Guid();
            Components = new List<AgentComponent>();
            events = new List<EventBus>()
        {
            new EventBus(),
            new EventBus(),
            new EventBus()
        };
        }

        public void Start()
        {
            var components = gameObject.GetComponents<AgentComponent>();

            foreach (AgentComponent component in components)
            {
                component.enabled = true;
                Components.Add(component);
            }
        }

        public void AddComponent<T>() where T : AgentComponent
        {
            var component = gameObject.AddComponent<T>();
            component.enabled = true;
            Components.Add(component);
        }

        public void AddListener(EventRegistrationData eventData)
        {
            if (events == null)
            {
                return;
            }

            switch (eventData.Priority)
            {
                case EventQueuePriority.High:
                    events[0].RegisterEvent(eventData);
                    break;
                case EventQueuePriority.Normal:
                    events[1].RegisterEvent(eventData);
                    break;
                case EventQueuePriority.Low:
                    events[2].RegisterEvent(eventData);
                    break;
            }
        }

        public void RemoveListener(EventRegistrationData eventData)
        {
            if (events == null)
            {
                return;
            }

            foreach (EventBus bus in events)
            {
                bus.TryRemoveEvent(eventData);
            }
        }

        public void Broadcast(string eventType, EventParameter parameters)
        {
            for(int i = 0; i < events.Count; i++)
            {
                events[i].BroadcastEvent(eventType, parameters);
            }
        }

        public void BroadcastEvent(string eventType)
        {
            Broadcast(eventType, null);
        }
    }
}