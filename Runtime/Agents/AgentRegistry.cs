using IronCarpStudios.AES.Agents;
using System.Collections.Generic;

public static class AgentCache
{
    private static Dictionary<int, Agent> cache { get; set; }

    static AgentCache()
    {
        cache = new Dictionary<int, Agent>();
    }

    public static bool TryAddAgent(int id, Agent agent)
    {
        if (!cache.ContainsKey(id))
        {
            cache.Add(id, agent);
            return true;
        }

        return false;
    }

    public static bool RemoveAgent(int id)
    {
        return cache.Remove(id);
    }

    public static bool TryGetAgent(int id, out Agent agent)
    {
        return cache.TryGetValue(id, out agent);
    }
}
