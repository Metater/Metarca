using System.Numerics;

namespace Metarca.Physics.Public;

internal class EntityListeners : IEntityListener, IRegistry<IEntityListener>
{
    private readonly List<IEntityListener> listeners;

    public EntityListeners(List<IEntityListener> listeners)
    {
        this.listeners = listeners;
    }

    public bool Add(IEntityListener listener)
    {
        if (listeners.Contains(listener)) return false;
        listeners.Add(listener);
        return true;
    }
    public bool Remove(IEntityListener listener)
    {
        return listeners.Remove(listener);
    }

    public void OnEarlyStep(Entity entity, double time, double deltaTime) => listeners.ForEach(l => l.OnEarlyStep(entity, time, deltaTime));
    public void OnLateStep(Entity entity, double time, double deltaTime) => listeners.ForEach(l => l.OnLateStep(entity, time, deltaTime));
    public void OnMove(Entity entity, Vector2? oldPosition, Vector2 newPosition) => listeners.ForEach(l => l.OnMove(entity, oldPosition, newPosition));
    public void OnAccelerate(Entity entity, Vector2? oldVelocity, Vector2 newVelocity) => listeners.ForEach(l => l.OnAccelerate(entity, oldVelocity, newVelocity));
    public void OnSelfRepulseOther(Entity self, Entity other) => listeners.ForEach(l => l.OnSelfRepulseOther(self, other));
    public void OnOtherRepulseSelf(Entity self, Entity other) => listeners.ForEach(l => l.OnOtherRepulseSelf(self, other));
    public void OnSelfStopOther(Entity self, Entity other, StopDirection direction) => listeners.ForEach(l => l.OnSelfStopOther(self, other, direction));
    public void OnOtherStopSelf(Entity self, Entity other, StopDirection direction) => listeners.ForEach(l => l.OnOtherStopSelf(self, other, direction));
    public void OnSelfTriggerOther(Entity self, Entity other) => listeners.ForEach(l => l.OnSelfTriggerOther(self, other));
    public void OnOtherTriggerSelf(Entity self, Entity other) => listeners.ForEach(l => l.OnOtherTriggerSelf(self, other));
    public void OnBoundsStop(Entity entity) => listeners.ForEach(l => l.OnBoundsStop(entity));
}