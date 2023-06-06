using LiteNetLib.Utils;
using LiteNetLib;

namespace Metarca.Server;

public class NetOut
{
    private readonly NetManager netManager;
    private readonly NetPacketProcessor packetProcessor;
    private readonly NetDataWriter writer = new();

    public NetOut(NetManager netManager, NetPacketProcessor packetProcessor)
    {
        this.netManager = netManager;
        this.packetProcessor = packetProcessor;
    }

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
}