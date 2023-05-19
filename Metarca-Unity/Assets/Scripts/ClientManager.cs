using LiteNetLib;
using LiteNetLib.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientManager : MonoBehaviour, INetEventListener
{
    private NetManager netManager;
    private NetPacketProcessor packetProcessor;
    private NetDataWriter writer;

    private void Awake()
    {
        netManager = new(this)
        {
            AutoRecycle = false,
            PacketPoolSize = 1000
        };
        packetProcessor = new();
        writer = new();

        netManager.Start();
        netManager.Connect("localhost", 7777, "Metarca");
    }

    private void Update()
    {
        netManager.PollEvents();
    }

    private void OnApplicationQuit()
    {
        netManager.Stop();
    }

    public void SendPacket<T>(T packet, DeliveryMethod deliveryMethod) where T : class, new()
    {
        var server = netManager.FirstPeer;
        if (server == null)
            return;
        writer.Reset();
        packetProcessor.Write(writer, packet);
        server.Send(writer, deliveryMethod);
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
