using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using LiteNetLib.Utils;

namespace Metarca.Server;

public class ServerManager : INetEventListener
{
    public readonly NetManager netManager;
    public readonly NetPacketProcessor packetProcessor = new();
    private readonly NetDataWriter writer = new();

    public ServerManager()
    {
        netManager = new(this);
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

    public void OnConnectionRequest(ConnectionRequest request)
    {
        request.Accept();
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
        
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
        
    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
    {
        packetProcessor.ReadAllPackets(reader);
        reader.Recycle();
    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        
    }

    public void OnPeerConnected(NetPeer peer)
    {
        
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        
    }
}