using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Metarca.Server.Physics.Types;

public interface IEntityListener : ISteppable, ITickable
{
    public virtual void OnChangedCell(uint? oldCellIndex, uint newCellIndex) { }
    public virtual void OnMoved(Vector2? oldPosition, Vector2 newPosition) { }
    public virtual void OnAccelerated(Vector2? oldVelocity, Vector2 newVelocity) { }
    public virtual void OnRepulsedOther(Entity repulsee) { }
    public virtual void OnRepulsedSelf(Entity repulsor) { }
    public virtual void OnStoppedOther(Entity stopee, StopDirection direction) { }
    public virtual void OnStoppedSelf(Entity stopper, StopDirection direction) { }
    public virtual void OnStoppedByBounds(StopDirection direction) { }
}