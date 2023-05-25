using System.Numerics;

namespace Metarca.Server.Physics.Types;

public interface IEntityListener
{
    /// <summary>
    /// Called before this entity's Step() logic begins
    /// </summary>
    /// <param name="time"></param>
    /// <param name="deltaTime"></param>
    public virtual void OnEarlyStep(double time, double deltaTime) { }
    /// <summary>
    /// Called after this entity's Step() logic ends
    /// </summary>
    /// <param name="time"></param>
    /// <param name="deltaTime"></param>
    public virtual void OnLateStep(double time, double deltaTime) { }
    /// <summary>
    /// Called on this entity's Tick()
    /// </summary>
    public virtual void OnTick() { }
    /// <summary>
    /// Called when this entity's changes cells
    /// </summary>
    /// <param name="oldCellIndex"></param>
    /// <param name="newCellIndex"></param>
    public virtual void OnCellChange(uint? oldCellIndex, uint newCellIndex) { }
    /// <summary>
    /// Called when this entity's position changes
    /// </summary>
    /// <param name="oldPosition"></param>
    /// <param name="newPosition"></param>
    public virtual void OnMove(Vector2? oldPosition, Vector2 newPosition) { }
    /// <summary>
    /// Called when this entity's velocity changes
    /// </summary>
    /// <param name="oldVelocity"></param>
    /// <param name="newVelocity"></param>
    public virtual void OnAccelerate(Vector2? oldVelocity, Vector2 newVelocity) { }
    /// <summary>
    /// Called when this entity repulses another entity
    /// </summary>
    /// <param name="repulsee"></param>
    public virtual void OnOtherRepulse(Entity repulsee) { }
    /// <summary>
    /// Called when this entity is repulsed by another entity
    /// </summary>
    /// <param name="repulsor"></param>
    public virtual void OnSelfRepulse(Entity repulsor) { }
    /// <summary>
    /// Called when another entity initiates a collision with this entity
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="direction"></param>
    public virtual void OnOtherCollide(Entity collider, StopDirection direction) { }
    /// <summary>
    /// Called when this entity stops due to bounds
    /// </summary>
    /// <param name="direction"></param>
    public virtual void OnBoundsStop(StopDirection direction) { }
}