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
            //disable the component here and let the agent start them once it is initialized.
            this.enabled = false;
            Priority = EventQueuePriority.Normal;
        }

        public virtual void OnEnable()
        {
            agent = GetComponent<Agent>();

            if(agent == null)
            {
                this.enabled = false;
            }

            Subscribe();
        }

        public virtual void OnDisable()
        {
            if (agent != null)
            {
                Unsubscribe();
            }

            agent = null;
        }

        protected virtual void Subscribe() { }
        protected virtual void Unsubscribe() { }
    }
}