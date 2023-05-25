using System.Numerics;

namespace Metarca.Server.Physics.Types;

public class EntityEvents : IEntityListener
{
    private readonly List<IEntityListener> listeners;

    public EntityEvents(List<IEntityListener> listeners)
    {
        this.listeners = listeners;
    }

    #region Listener Lifetime
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
    #endregion

    public void OnEarlyStep(double time, double deltaTime) => listeners.ForEach(l => l.OnEarlyStep(time, deltaTime));
    public void OnLateStep(double time, double deltaTime) => listeners.ForEach(l => l.OnLateStep(time, deltaTime));
    public void OnTick() => listeners.ForEach(l => l.OnTick());
    public void OnCellChange(uint? oldCellIndex, uint newCellIndex) => listeners.ForEach(l => l.OnCellChange(oldCellIndex, newCellIndex));
    public void OnMove(Vector2? oldPosition, Vector2 newPosition) => listeners.ForEach(l => l.OnMove(oldPosition, newPosition));
    public void OnAccelerate(Vector2? oldVelocity, Vector2 newVelocity) => listeners.ForEach(l => l.OnAccelerate(oldVelocity, newVelocity));
    public void OnOtherRepulse(Entity repulsee) => listeners.ForEach(l => l.OnOtherRepulse(repulsee));
    public void OnSelfRepulse(Entity repulsor) => listeners.ForEach(l => l.OnSelfRepulse(repulsor));
    public void OnStaticStop(StaticCollider collider, StopDirection direction) => listeners.ForEach(l => l.OnStaticStop(collider, direction));
    public void OnBoundsStop(StopDirection direction) => listeners.ForEach(l => l.OnBoundsStop(direction));
}