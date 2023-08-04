using System;
using System.Collections.Generic;

namespace Metarca.Shared.Collections;

public interface IEphemeralQueues
{
    public void Enqueue<T>(T item) where T : class;
    public IEnumerable<T> Get<T>() where T : class;
    public IEnumerable<(Type, IEnumerable<object>)> GetNonEmpty();
    public void ClearAll();
}