using System;
using System.Collections.Generic;

namespace Proelium.Shared.Collections;

public interface IEphemeralQueues
{
    public void Enqueue<T>(T item) where T : class, new();
    public IEnumerable<T> Get<T>() where T : class, new();
    public IEnumerable<(Type, IEnumerable<object>)> GetNonEmpty();
    public void ClearAll();
    public void PoolAll(Pools pools);
}