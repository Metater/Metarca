using System;
using System.Collections.Generic;

namespace Metarca.Shared.Collections;

public class Singletons
{
    private readonly Dictionary<Type, object> singletons = new();

    public void Add<T>(T singleton) where T : class
    {
        singletons.Add(typeof(T), singleton);
    }

    public T Get<T>() where T : class
    {
        return (T)singletons[typeof(T)];
    }
}