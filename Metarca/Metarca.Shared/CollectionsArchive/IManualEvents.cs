using System;
using System.Collections.Generic;

namespace Proelium.Shared.Collections;

public interface IManualEvents
{
    public void Add<T>(T @event) where T : class, new();
    public IEnumerable<T> Get<T>() where T : class, new();
    public bool Clear<T>() where T : class, new();
    public IEnumerable<Type> GetOversized<T>(int limit) where T : class, new();
}