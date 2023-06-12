using System;
using System.Collections.Generic;

namespace Metarca.Shared.Collections;

public class GenericPools
{
    private readonly int capacity;
    private readonly Dictionary<Type, Stack<object>> pools = new();

    public GenericPools(int capacity)
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
}