using System.Numerics;

namespace Metarca.Server.Physics.Types;

public class EntityEvents : IEntityListener
{
    private readonly List<IEntityListener> listeners;

    public EntityEvents(List<IEntityListener> listeners)
    {
        this.listeners = listeners;
    }

    public bool SubscribeListener(IEntityListener listener)
    {
        if (listeners.Contains(listener)) return false;
        listeners.Add(listener);
        return true;
    }
    public bool UnsubscribeListener(IEntityListener listener)
    {
        return listeners.Remove(listener);
    }

    public void OnStepped(double time, double deltaTime) => listeners.ForEach(l => l.OnStepped(time, deltaTime));
    public void OnTicked(double time, ulong tickId) => listeners.ForEach(l => l.OnTicked(time, tickId));
    public void OnChangedCell(uint? oldCellIndex, uint newCellIndex) => listeners.ForEach(l => l.OnChangedCell(oldCellIndex, newCellIndex));
    public void OnMoved(Vector2? oldPosition, Vector2 newPosition) => listeners.ForEach(l => l.OnMoved(oldPosition, newPosition));
    public void OnAccelerated(Vector2? oldVelocity, Vector2 newVelocity) => listeners.ForEach(l => l.OnAccelerated(oldVelocity, newVelocity));
    public void OnRepulsedOther(Entity repulsee) => listeners.ForEach(l => l.OnRepulsedOther(repulsee));
    public void OnRepulsedSelf(Entity repulsor) => listeners.ForEach(l => l.OnRepulsedSelf(repulsor));
    public void OnStoppedOther(Entity stopee, StopDirection direction) => listeners.ForEach(l => l.OnStoppedOther(stopee, direction));
    public void OnStoppedSelf(Entity stopper, StopDirection direction) => listeners.ForEach(l => l.OnStoppedSelf(stopper, direction));
    public void OnStoppedByBounds(StopDirection direction) => listeners.ForEach(l => l.OnStoppedByBounds(direction));
}