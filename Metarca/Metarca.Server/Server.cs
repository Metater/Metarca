using Metarca.Shared;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metarca.Server.Physics;
using Metarca.Server.Physics.Config;

namespace Metarca.Server;

public class Server : ITickable
{
    private readonly ServerManager serverManager = new();
    private readonly Zone zone = new();
    private readonly Entity entity;

    public Server()
    {
        serverManager.netManager.Start(7777);

        serverManager.packetProcessor.RegisterNestedType<DebugEntity>();

        entity = new(zone, new(), new(), new TestEntityListener());
        entity.WithBounds(new(new(new(-10, -5), new(10, 5))));
    }

    public void PollEvents()
    {
        serverManager.netManager.PollEvents();
    }

    public void Tick(double time, ulong tickId)
    {
        if (tickId % Constants.TicksPerSecond == 0)
        {
            // Runs once per second
        }

        serverManager.SendPacketToAll(new TimePacket()
        {
            Time = time
        }, DeliveryMethod.Unreliable);

        if (tickId % Constants.TicksPerSecondMultiplier == 0)
        {

        }

        entity.AddForce(new(5 * MathF.Cos((float)time), -0.1f), Constants.SecondsPerTick);

        zone.Step(time, Constants.SecondsPerTick);
        zone.Tick(time, tickId);



        var debugEntity = new DebugEntity
        {
            Id = 0,
            X = entity.Position.X,
            Y = entity.Position.Y
        };

        serverManager.SendPacketToAll(new DebugEntityPacket()
        {
            Entities = new DebugEntity[] { debugEntity }
        }, DeliveryMethod.Unreliable);





        Console.Title = $"Tick Id: {tickId}, TPS: {Constants.TicksPerSecond}, Position: {entity.Position}, Velocity: {entity.Velocity}";
    }

    public void Stop()
    {
        serverManager.netManager.Stop();
    }
}
