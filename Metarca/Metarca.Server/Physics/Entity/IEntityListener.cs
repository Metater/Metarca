using System.Numerics;

namespace Metarca.Server.Physics.Types;

public interface IEntityListener
{
    public virtual void OnStepped(double time, double deltaTime) { }
    public virtual void OnTicked(double time, ulong tickId) { }
    public virtual void OnChangedCell(uint? oldCellIndex, uint newCellIndex) { }
    public virtual void OnMoved(Vector2? oldPosition, Vector2 newPosition) { }
    public virtual void OnAccelerated(Vector2? oldVelocity, Vector2 newVelocity) { }
    public virtual void OnRepulsedOther(Entity repulsee) { }
    public virtual void OnRepulsedSelf(Entity repulsor) { }
    public virtual void OnStoppedOther(Entity stopee, StopDirection direction) { }
    public virtual void OnStoppedSelf(Entity stopper, StopDirection direction) { }
    public virtual void OnStoppedByBounds(StopDirection direction) { }
}