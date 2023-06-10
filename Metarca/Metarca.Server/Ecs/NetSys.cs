using LiteNetLib;
using LiteNetLib.Utils;

namespace Metarca.Server.Ecs;

public abstract class DepsSys : Sys
{
    public record Deps(EventBasedNetListener Listener, NetManager NetManager, NetPacketProcessor PacketProcessor, NetOut NetOut);

    protected Deps deps;
    protected readonly EventBasedNetListener listener;
    protected readonly NetManager netManager;
    protected readonly NetPacketProcessor packetProcessor;
    private readonly NetOut netOut;

    public DepsSys(Deps deps, Sys? parent = null) : base(parent)
    {
        this.deps = deps;
        listener = deps.Listener;
        netManager = deps.NetManager;
        packetProcessor = deps.PacketProcessor;
        netOut = deps.NetOut;
    }

    public void SendPacketToPeer<T>(NetPeer peer, T packet, DeliveryMethod deliveryMethod) where T : class, new()
    {
        netOut.SendPacketToPeer(peer, packet, deliveryMethod);
    }
    public void SendPacketToAll<T>(T packet, DeliveryMethod deliveryMethod) where T : class, new()
    {
        netOut.SendPacketToAll(packet, deliveryMethod);
    }
}