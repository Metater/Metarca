using Metarca.Physics.Partitioning;
using Metarca.Physics.Public;
using Metarca.Physics.Simulation;

namespace Metarca.Physics;

public class Zone : IRegistry<Entity>
{
    private readonly IEntityListener listener;
    private readonly PartitionSystem partitionSystem = new();
    private readonly SimulationSystem stepSystem;

    private readonly List<Entity> entities = new();
    private readonly List<Entity> stepEntities = new();

    public Zone(IEntityListener listener)
    {
        this.listener = listener;
        stepSystem = new(listener, partitionSystem);
    }

    public bool Add(Entity entity)
    {
        if (entities.Contains(entity)) return false;
        entities.Add(entity);
        return true;
    }
    public bool Remove(Entity entity)
    {
        return entities.Remove(entity);
    }

    public void Step(double time, double deltaTime)
    {
        stepEntities.Clear();
        stepEntities.AddRange(entities);

        foreach (var entity in stepEntities)
        {
            bool moved = stepSystem.Simulate(entity, time, deltaTime);
        }
    }
}