using LiteNetLib;
using System;
using System.Collections.Generic;

namespace Metarca.Shared;

public class PacketPool
{
    private readonly int initialSize;
    private readonly Dictionary<Type, Stack<IPoolable>> pool = new();

    public PacketPool(int initialSize)
    {
        this.initialSize = initialSize;
    }   

    public T Pop<T>() where T : IPoolable, new()
    {
        Type type = typeof(T);
        if (!pool.TryGetValue(type, out var stack))
        {
            stack = new Stack<T>(initialSize);
            pool[type] = stack;
        }

        return stack.Pop();
    }
}