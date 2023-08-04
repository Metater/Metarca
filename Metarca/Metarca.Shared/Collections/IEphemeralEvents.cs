using System.Collections.Generic;

namespace Metarca.Shared.Collections;

public interface IEphemeralEvents
{
    public void Add<T>(T @event) where T : class;
    public IEnumerable<T> Get<T>() where T : class;
    public void ClearAll();
}