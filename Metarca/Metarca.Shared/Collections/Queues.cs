using System;
using System.Collections.Generic;
using System.Linq;

namespace Metarca.Shared.Collections;

public class Queues
{
    private readonly Dictionary<Type, Queue<object>> queues = new();

    public void Enqueue<T>(T item) where T : class
    {
        Type type = typeof(T);
        if (!queues.TryGetValue(type, out var queue))
        {
            queue = new();
            queues[type] = queue;
        }

        queue.Enqueue(item);
    }

    public IEnumerable<T> DequeueAll<T>() where T : class
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
            .Select(kvp => (kvp.Key, kvp.Value.AsEnumerable()));
    }
}