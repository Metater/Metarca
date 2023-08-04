using System;
using System.Collections.Generic;

namespace Proelium.Shared.Collections;

public interface IManualQueues
{
    public void Enqueue<T>(T item) where T : class, new();
    public IEnumerable<T> Get<T>() where T : class, new();
    public IEnumerable<Type> GetOversized<T>(int limit) where T : class, new();
}