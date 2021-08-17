using IronCarpStudios.AES.Agents;
using System;

namespace IronCarpStudios.AES.Events
{
    public delegate void AgentEventHandler(Agent sender, AgentEventArgs args);

    public abstract class AgentEventArgs : EventArgs { }
}