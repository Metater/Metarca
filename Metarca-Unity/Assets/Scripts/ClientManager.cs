using LiteNetLib;
using LiteNetLib.Utils;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientManager : MonoBehaviour, INetEventListener
{
    public static readonly NetPacketProcessor packetProcessor = new();
    private static readonly NetDataWriter writer = new();
    public static NetManager NetManager { get; private set; }

    private void Awake()
    {
        NetManager = new(this);
        NetManager.Start();
        NetManager.Connect("localhost", 7777, "Metarca");
    }

    private void Update()
    {
        NetManager.PollEvents();
    }

    private void OnApplicationQuit()
    {
        NetManager.Stop();
    }

    public static bool SendPacket<T>(T packet, DeliveryMethod deliveryMethod) where T : class, new()
    {
        var server = NetManager.FirstPeer;
        if (server == null) return false;
        writer.Reset();
        packetProcessor.Write(writer, packet);
        server.Send(writer, deliveryMethod);
        return true;
    }

    public void OnConnectionRequest(ConnectionRequest request)
    {
        
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {

    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {

    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
    {
        packetProcessor.ReadAllPackets(reader, peer);
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
