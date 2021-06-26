using IronCarp.AES.Events;
using UnityEngine;

namespace IronCarp.AES.Agents
{
    public abstract class AgentComponent : MonoBehaviour
    {
        protected Agent agent;
        protected EventQueuePriority Priority;


        public virtual void Awake()
        {
            agent = GetComponent<Agent>();
            this.enabled = false;
            Priority = EventQueuePriority.Normal;
        }

        public virtual void OnEnable()
        {
            Subscribe();
        }

        public virtual void OnDisable()
        {
            if (agent != null)
            {
                Unsubscribe();
            }
        }

        protected virtual void Subscribe() { }
        protected virtual void Unsubscribe() { }
    }
}