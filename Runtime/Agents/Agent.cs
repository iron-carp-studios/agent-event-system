using IronCarpStudios.AES.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IronCarpStudios.AES.Agents
{
    public class Agent : MonoBehaviour
    {
        public Guid AgentId;
        protected Dictionary<string, AgentStatComponent> Stats { get; private set; }
        private List<AgentComponent> Components;
        private List<EventBus> events;

        private void Awake()
        {
            AgentId = new Guid();
            Components = new List<AgentComponent>();
            var statComponents = GetComponents<AgentStatComponent>();
            Stats = new Dictionary<string, AgentStatComponent>();

            foreach (AgentStatComponent stat in statComponents)
            {
                if (!Stats.ContainsKey(stat.StatName))
                {
                    Stats.Add(stat.StatName, stat);
                }
            }

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

        public virtual void OnEnable()
        {
            AgentCache.TryAddAgent(gameObject.GetInstanceID(), this);
        }

        public virtual void OnDisable()
        {
            AgentCache.RemoveAgent(gameObject.GetInstanceID());
        }

        public void AddComponent<T>() where T : AgentComponent
        {
            var component = gameObject.AddComponent<T>();
            component.enabled = true;
            Components.Add(component);
        }

        public bool TryGetStat(string key, out AgentStatComponent stat)
        {
            return Stats.TryGetValue(key, out stat);
        }
        public void AddListener(string eventKey, Action<Agent, AgentEventArgs> action)
        {
            AddListener(new EventRegistrationData(eventKey, action));
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

        public void RemoveListener(string eventType, Action<Agent, AgentEventArgs> action)
        {
            RemoveListener(new EventRegistrationData(eventType, action));
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

        public void Broadcast(string eventType, AgentEventArgs parameters)
        {
            for(int i = 0; i < events.Count; i++)
            {
                events[i].BroadcastEvent(eventType, this, parameters);
            }
        }

        public void BroadcastEvent(string eventType)
        {
            Broadcast(eventType, null);
        }
    }
}