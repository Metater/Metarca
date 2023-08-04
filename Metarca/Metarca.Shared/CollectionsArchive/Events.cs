using System;
using System.Collections.Generic;
using System.Linq;

namespace Proelium.Shared.Collections;

public class Events : IEphemeralEvents, IManualEvents
{
    private readonly Dictionary<Type, List<object>> events = new();

    public void Add<T>(T @event) where T : class, new()
    {
        Type type = typeof(T);
        if (!events.TryGetValue(type, out var list))
        {
            list = new();
            events[type] = list;
        }

        list.Add(@event);
    }

    public IEnumerable<T> Get<T>() where T : class, new()
    {
        if (!events.TryGetValue(typeof(T), out var list))
        {
            yield break;
        }

        foreach (var @event in list)
        {
            yield return (T)@event;
        }
    }

    public void ClearAll()
    {
        foreach (var @event in events.Values)
        {
            @event.Clear();
        }
    }

    public bool Clear<T>() where T : class, new()
    {
        if (events.TryGetValue(typeof(T), out var list))
        {
            list.Clear();
            return true;
        }

        return false;
    }

    public IEnumerable<Type> GetOversized<T>(int limit) where T : class
    {
        return events
            .Where(kvp => kvp.Value.Count > limit)
            .Select(kvp => kvp.Key);
    }
}