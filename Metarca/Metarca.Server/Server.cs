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
    private readonly Entity entityA;
    private readonly Entity entityB;

    public Server()
    {
        serverManager.netManager.Start(7777);

        serverManager.packetProcessor.RegisterNestedType<DebugEntity>();

        serverManager.packetProcessor.RegisterNestedType<InputPacket.Data>();
        serverManager.packetProcessor.SubscribeReusable<InputPacket, NetPeer>(OnInputPacket);

        entityA = new Entity(zone, new(0, 1), new(), new TestEntityListener())
            .WithBounds(new(new(new(-10, -5), new(10, 5))))
            .WithRepulsion(new(true, true, 0.4f, 48, 3));

        entityB = new Entity(zone, new(0, 3), new(), new TestEntityListener())
            .WithBounds(new(new(new(-10, -5), new(10, 5))))
            .WithRepulsion(new(true, true, 0.4f, 48, 3));
    }

    public void PollEvents()
    {
        serverManager.netManager.PollEvents();
    }

    private int i;

    public void Tick(double time, ulong tickId)
    {
        if (tickId % Constants.TicksPerSecond == 0)
        {
            // Runs once per second
            Console.WriteLine(i);
            i = 0;
        }

        serverManager.SendPacketToAll(new TimePacket()
        {
            Time = time
        }, DeliveryMethod.Unreliable);

        if (tickId % Constants.TicksPerSecondMultiplier == 0)
        {

        }

        entityA.AddForce(new(-10f, -40f), Constants.SecondsPerTick);
        entityB.AddForce(new(-10, -40f), Constants.SecondsPerTick);

        zone.Step(time, Constants.SecondsPerTick);
        zone.Tick(time, tickId);



        var a = new DebugEntity
        {
            Id = 0,
            X = entityA.Position.X,
            Y = entityA.Position.Y
        };

        var b = new DebugEntity
        {
            Id = 1,
            X = entityB.Position.X,
            Y = entityB.Position.Y
        };

        serverManager.SendPacketToAll(new DebugEntityPacket()
        {
            Entities = new DebugEntity[] { a, b }
        }, DeliveryMethod.Unreliable);





        Console.Title = $"Tick Id: {tickId}, TPS: {Constants.TicksPerSecond}";
    }

    public void Stop()
    {
        serverManager.netManager.Stop();
    }

    private void OnInputPacket(InputPacket packet, NetPeer peer)
    {
        i++;
    }
}
