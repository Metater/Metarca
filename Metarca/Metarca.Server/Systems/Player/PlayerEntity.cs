using Metarca.Server.Physics;
using Metarca.Server.Physics.Types;
using Metarca.Shared.Packets;
using System.Numerics;

namespace Metarca.Server.Systems.Player;

public class PlayerEntity : Entity
{
    public InputPacket.Data LatestData { get; set; } = new();

    public PlayerEntity(Zone zone, Vector2 position, Vector2 velocity, params IEntityListener[] listeners) : base(zone, position, velocity, listeners)
    {
        // TODO Add prestep callback, so you can apply force before step happens, faster responses
    }
}