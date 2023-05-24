using LiteNetLib;

namespace Metarca.Server.Systems;

public class NetInSystem : System
{
    public NetInSystem(Deps deps) : base(deps)
    {
        listener.NetworkReceiveEvent += NetworkReceive;
    }

    private void NetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
    {
        packetProcessor.ReadAllPackets(reader);
    }
}