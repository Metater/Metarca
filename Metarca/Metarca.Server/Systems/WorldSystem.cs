using Metarca.Server.Physics;

namespace Metarca.Server.Systems;

public class WorldSystem : System
{
    public readonly Zone zone = new();

    public WorldSystem(Deps deps) : base(deps)
    {

    }


}