using IronCarp.AES.Agents;
using System;

namespace IronCarp.AES.Events
{
    public delegate void AgentEventHandler(Agent sender, AgentEventArgs args);

    public abstract class AgentEventArgs : EventArgs { }
}