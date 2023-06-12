using System;
using System.Collections.Generic;

namespace Metarca.Shared.Collections;

public class GenericEvents
{
    private readonly Dictionary<Type, List<object>> events = new();

    public void Add<T>(T item) where T : class
    {
        Type type = typeof(T);
        if (!events.TryGetValue(type, out var list))
        {
            list = new();
            events[type] = list;
        }

        list.Add(item);
    }

    public IEnumerable<T> Get<T>() where T : class
    {
        if (events.TryGetValue(typeof(T), out var list))
        {
            foreach (var item in list)
            {
                yield return (T)item;
            }
        }

        yield break;
    }

    public void Clear()
    {
        foreach (var @event in events.Values)
        {
            @event.Clear();
        }
    }
}