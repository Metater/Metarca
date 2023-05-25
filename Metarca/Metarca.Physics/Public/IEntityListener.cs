using System.Numerics;

namespace Metarca.Physics.Public;

public interface IEntityListener
{
    public virtual void OnEarlyStep(Entity entity, double time, double deltaTime) { }
    public virtual void OnLateStep(Entity entity, double time, double deltaTime) { }
    public virtual void OnMove(Entity entity, Vector2? oldPosition, Vector2 newPosition) { }
    public virtual void OnAccelerate(Entity entity, Vector2? oldVelocity, Vector2 newVelocity) { }
    public virtual void OnSelfRepulseOther(Entity self, Entity other) { }
    public virtual void OnOtherRepulseSelf(Entity self, Entity other) { }
    public virtual void OnSelfStopOther(Entity self, Entity other, StopDirection direction) { }
    public virtual void OnOtherStopSelf(Entity self, Entity other, StopDirection direction) { }
    public virtual void OnSelfTriggerOther(Entity self, Entity other) { }
    public virtual void OnOtherTriggerSelf(Entity self, Entity other) { }
    public virtual void OnBoundsStop(Entity entity) { }
}