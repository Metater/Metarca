namespace Metarca.Physics.Simulation;

internal class SimulationSystem
{
    private readonly ISurroundingProvider surroundingProvider;
    
    internal SimulationSystem(ISurroundingProvider surroundingProvider)
    {
        this.surroundingProvider = surroundingProvider;
    }

    internal bool Simulate(Entity entity, double time, double deltaTime)
    {

    }
}