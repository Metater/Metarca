using Metarca.Physics.Public;
using System.Numerics;

namespace Metarca.Physics.Simulation;

internal class SimulationSystem
{
    private readonly IEntityListener listener;
    private readonly ISurroundingProvider surroundingProvider;
    
    internal SimulationSystem(IEntityListener listener, ISurroundingProvider surroundingProvider)
    {
        this.listener = listener;
        this.surroundingProvider = surroundingProvider;
    }

    internal bool Simulate(Entity entity, double time, double deltaTime)
    {
        listener.OnEarlySimulate(entity, time, deltaTime);

        if (entity.repulsion.canBeRepulsed)
        {
            Vector2 force = Vector2.Zero;
            foreach (var other in surroundingProvider.GetSurroundingEntities(entity.CellId!.Value))
            {
                if (other == entity) continue;
                if (!other.repulsion.canRepulseOthers) continue;
                force += 
            }
        }

        listener.OnLateSimulate(entity, time, deltaTime);
    }
}