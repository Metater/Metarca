using Metarca.Shared;
using LiteNetLib;
using Metarca.Server.Physics;
using Metarca.Shared.Packets;
using Metarca.Server.Interfaces;
using LiteNetLib.Utils;

namespace Metarca.Server;

public class Server : ITickable
{
    private readonly EventBasedNetListener listener = new();
    private readonly NetManager netManager;
    private readonly NetPacketProcessor packetProcessor = new();
    private readonly NetDataWriter writer = new();

    private readonly Zone zone = new();
    private readonly Entity entityA;
    private readonly Entity entityB;

    public Server()
    {
        listener.ConnectionRequestEvent += request => request.Accept();
        listener.NetworkReceiveEvent += (peer, reader, channelNumber, deliveryMethod) => packetProcessor.ReadAllPackets(reader);

        netManager = new(listener)
        {
            AutoRecycle = true
        };

        packetProcessor.RegisterNestedType<DebugEntity>();

        packetProcessor.RegisterNestedType<InputPacket.Data>();
        packetProcessor.SubscribeReusable<InputPacket, NetPeer>(OnInputPacket);

        entityA = new Entity(zone, new(0, 1), new(), new TestEntityListener())
            .WithBounds(new(new(new(-10, -5), new(10, 5))))
            .WithRepulsion(new(true, true, 0.4f, 48, 3));

        entityB = new Entity(zone, new(0, 3), new(), new TestEntityListener())
            .WithBounds(new(new(new(-10, -5), new(10, 5))))
            .WithRepulsion(new(true, true, 0.4f, 48, 3));

        netManager.Start(7777);
    }

    public void PollEvents()
    {
        netManager.PollEvents();
    }

    public void Tick(double time, ulong tickId)
    {
        if (tickId % Constants.TicksPerSecond == 0)
        {
            // Runs once per second
        }

        SendPacketToAll(new TimePacket()
        {
            Time = time
        }, DeliveryMethod.Unreliable);

        if (tickId % Constants.TicksPerSecondMultiplier == 0)
        {

        }

        entityA.AddForce(new(-2f, -800f), Constants.SecondsPerTick);
        entityB.AddForce(new(-3, -800f), Constants.SecondsPerTick);

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

        SendPacketToAll(new DebugEntityPacket()
        {
            Entities = new DebugEntity[] { a, b }
        }, DeliveryMethod.Unreliable);





        Console.Title = $"Tick Id: {tickId}, TPS: {Constants.TicksPerSecond}";
    }

    public void Stop()
    {
        netManager.Stop();
    }

    private void OnInputPacket(InputPacket packet, NetPeer peer)
    {
        Console.WriteLine(packet.Input.WASD);
    }

    #region Send Packet To
    public void SendPacketToPeer<T>(NetPeer peer, T packet, DeliveryMethod deliveryMethod) where T : class, new()
    {
        writer.Reset();
        packetProcessor.Write(writer, packet);
        peer.Send(writer, deliveryMethod);
    }
    public void SendPacketToAll<T>(T packet, DeliveryMethod deliveryMethod) where T : class, new()
    {
        writer.Reset();
        packetProcessor.Write(writer, packet);
        netManager.SendToAll(writer, deliveryMethod);
    }
    #endregion
}
