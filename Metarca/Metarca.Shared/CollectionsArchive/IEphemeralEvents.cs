using System.Collections.Generic;

namespace Proelium.Shared.Collections;

public interface IEphemeralEvents
{
    public void Add<T>(T @event) where T : class, new();
    public IEnumerable<T> Get<T>() where T : class, new();
    public void ClearAll();
    public void PoolAll(Pools pools);
}