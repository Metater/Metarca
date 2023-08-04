using System;
using System.Collections.Generic;

namespace Metarca.Shared.Collections;

public interface IManualEvents
{
    public void Add<T>(T @event) where T : class;
    public IEnumerable<T> Get<T>() where T : class;
    public bool Clear<T>() where T : class;
    public IEnumerable<Type> GetOversized<T>(int limit) where T : class;
}