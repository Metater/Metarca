using Metarca.Server.Physics;
using System.Numerics;

namespace Metarca.Server.Systems.Player;

public class PlayerInitSystem : System
{
    public PlayerInitSystem(Deps deps) : base(deps)
    {

    }

    public PlayerData ConstructPlayerData()
    {
        return new()
        {
            Entity = ConstructPlayerEntity()
        };
    }

    private PlayerEntity ConstructPlayerEntity()
    {
        Zone zone = Get<WorldSystem>().zone;
        return new(zone, Vector2.Zero, Vector2.Zero);
    }
}
