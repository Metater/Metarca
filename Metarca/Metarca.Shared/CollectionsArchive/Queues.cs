using System;
using System.Collections.Generic;
using System.Linq;

namespace Proelium.Shared.Collections;
    
public class Queues : IEphemeralQueues, IManualQueues
{
    private readonly Dictionary<Type, Queue<object>> queues = new();

    public void Enqueue<T>(T item) where T : class, new()
    {
        Type type = typeof(T);
        if (!queues.TryGetValue(type, out var queue))
        {
            queue = new();
            queues[type] = queue;
        }

        queue.Enqueue(item);
    }

    public IEnumerable<T> Get<T>() where T : class, new()
    {
        if (!queues.TryGetValue(typeof(T), out var queue))
        {
            yield break;
        }

        while (queue.TryDequeue(out var item))
        {
            yield return (T)item;
        }
    }

    public IEnumerable<(Type, IEnumerable<object>)> GetNonEmpty()
    {
        return queues
            .Where(kvp => kvp.Value.Count > 0)
            .Select(kvp => (kvp.Key, (IEnumerable<object>)kvp.Value));
    }

    public void ClearAll()
    {
        foreach (var queue in queues.Values)
        {
            queue.Clear();
        }
    }

    public void PoolAll(Pools pools)
    {
        foreach (var queue in queues.Values)
        {

        }
    }

    public IEnumerable<Type> GetOversized<T>(int limit) where T : class
    {
        return queues
            .Where(kvp => kvp.Value.Count > limit)
            .Select(kvp => kvp.Key);
    }
}