using Metarca.Server.Physics;
using Metarca.Shared;

namespace Metarca.Server.Systems;

public class WorldSystem : System
{
    public readonly Zone zone = new();

    public WorldSystem(Deps deps) : base(deps)
    {

    }

    public override void Tick()
    {
        zone.Step(Time.TickTime, Constants.SecondsPerTick);
    }
}