using System;
using System.Collections.Generic;

namespace Metarca.Shared.Collections;

public interface IManualQueues
{
    public void Enqueue<T>(T item) where T : class;
    public IEnumerable<T> Get<T>() where T : class;
    public IEnumerable<Type> GetOversized<T>(int limit) where T : class;
}