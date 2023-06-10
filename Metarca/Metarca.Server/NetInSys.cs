using LiteNetLib;
using Metarca.Server.Ecs;

namespace Metarca.Server;

public class NetInSys : DepsSys
{
    public NetInSys(Deps deps, Sys? parent = null) : base(deps, parent)
    {

    }

    protected override void Awake()
    {
        listener.NetworkReceiveEvent += Listener_NetworkReceiveEvent;
    }

    private void Listener_NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        packetProcessor.ReadAllPackets(reader, peer);
    }
}