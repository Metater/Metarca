using Metarca.Server.Physics;
using Metarca.Server.Physics.Types;
using Metarca.Shared;
using Metarca.Shared.Packets;
using System.Numerics;

namespace Metarca.Server.Systems.Player;

public class PlayerEntity : Entity, IEntityListener
{
    private const double InputTimeoutSeconds = 0.2f;
    private const float MovementForceMultiplier = 40;
    private (double time, InputPacket.Data data) latestInput = new();

    public PlayerEntity(Zone zone, Vector2 position, Vector2 velocity, params IEntityListener[] listeners) : base(zone, position, velocity, listeners)
    {

    }

    public void OnEarlyStep(double time, double deltaTime)
    {
        // Timeout any old input
        if (Time.TickTime - latestInput.time > InputTimeoutSeconds)
        {
            latestInput = (Time.TickTime, new());
        }

        InputPacket.WASD wasd = latestInput.data.WASD;

        // Determine movement force
        Vector2 force = Vector2.Zero;
        if ((wasd & InputPacket.WASD.W) == InputPacket.WASD.W)
        {
            force += new Vector2(0, 1);
        }
        if ((wasd & InputPacket.WASD.A) == InputPacket.WASD.A)
        {
            force += new Vector2(-1, 0);
        }
        if ((wasd & InputPacket.WASD.S) == InputPacket.WASD.S)
        {
            force += new Vector2(0, -1);
        }
        if ((wasd & InputPacket.WASD.D) == InputPacket.WASD.D)
        {
            force += new Vector2(1, 0);
        }

        if (force.Length() != 0)
        {
            force /= force.Length();
            force *= MovementForceMultiplier;
            ApplyForce(force, Constants.SecondsPerTick);
        }
    }

    public void SupplyInputData(InputPacket.Data data) => latestInput = (Time.TickTime, data);
}