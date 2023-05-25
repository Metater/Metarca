namespace Metarca.Physics.Public;

public interface IEntityListener
{
    public virtual void OnEarlySimulate(Entity entity, double time, double deltaTime) { }
    public virtual void OnLateSimulate(Entity entity, double time, double deltaTime) { }
    public virtual void OnSelfRepulseOther(Entity self, Entity other) { }
    public virtual void OnOtherRepulseSelf(Entity self, Entity other) { }
    public virtual void OnSelfStopOther(Entity self, Entity other, StopDirection direction) { }
    public virtual void OnOtherStopSelf(Entity self, Entity other, StopDirection direction) { }
    public virtual void OnSelfTriggerOther(Entity self, Entity other) { }
    public virtual void OnOtherTriggerSelf(Entity self, Entity other) { }
    public virtual void OnBoundsStop(Entity entity) { }
}