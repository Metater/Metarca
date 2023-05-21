using Metarca.Server.Physics;
using Metarca.Server.Physics.Types;
using System.Numerics;

namespace Metarca.Server;

public class TestEntityListener : IEntityListener
{
    public void Step(double time, double deltaTime)
    {
        
    }

    public void Tick(double time, ulong tickId)
    {
        
    }

    public void OnChangedCell(uint? oldCellIndex, uint newCellIndex)
    {
        Console.WriteLine(newCellIndex);
    }
    public void OnMoved(Vector2? oldPosition, Vector2 newPosition)
    {
        // Console.WriteLine($"Moved to {newPosition}");
    }
    public void OnAccelerated(Vector2? oldVelocity, Vector2 newVelocity)
    {
        // Console.WriteLine($"Accelerated to {newVelocity}");
    }
    public void OnRepulsedOther(Entity repulsee)
    {

    }
    public void OnRepulsedSelf(Entity repulsor)
    {

    }
    public void OnStoppedOther(Entity stopee, StopDirection direction)
    {

    }
    public void OnStoppedSelf(Entity stopper, StopDirection direction)
    {

    }
    public void OnStoppedByBounds(StopDirection direction)
    {
        // Console.WriteLine(direction.ToString());
    }
}