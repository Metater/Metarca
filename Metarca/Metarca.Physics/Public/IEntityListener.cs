namespace Metarca.Physics.Public;

public interface IEntityListener
{
    public virtual void OnEarlySimulate(Entity entity, double time, double deltaTime) { }
    public virtual void OnRepulsion(Entity repulsee, Entity repulsor) { }
    public virtual void OnTrigger(Entity triggeree, Entity triggerer) { }
    public virtual void OnStop(Entity stopee, Entity stopper, StopDirection direction) { }
    public virtual void OnBoundsStop(Entity entity, StopDirection direction) { }
    public virtual void OnLateSimulate(Entity entity, double time, double deltaTime) { }
}