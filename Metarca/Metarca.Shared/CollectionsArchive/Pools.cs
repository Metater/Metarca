using System;
using System.Collections.Generic;

namespace Proelium.Shared.Collections;

public class Pools
{
    private readonly int capacity;
    private readonly Dictionary<Type, Stack<object>> pools = new();

    public Pools(int capacity)
    {
        this.capacity = capacity;
    }

    public T Pop<T>() where T : class, new()
    {
        Type type = typeof(T);
        if (!pools.TryGetValue(type, out var stack))
        {
            stack = new(capacity);
            for (int i = 0; i < capacity; i++)
            {
                stack.Push(new T());
            }
            pools[type] = stack;
        }

        if (!stack.TryPop(out var item))
        {
            return new T();
        }

        return (T)item;
    }

    public void Push<T>(T item) where T : class, new()
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        Type type = typeof(T);
        if (!pools.TryGetValue(type, out var stack))
        {
            return;
        }

        if (stack.Count < capacity)
        {
            stack.Push(item);
        }
    }

    public void PushObject(object item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        if (!pools.TryGetValue(item.GetType(), out var stack))
        {
            return;
        }

        if (stack.Count < capacity)
        {
            stack.Push(item);
        }
    }
}