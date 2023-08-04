using LiteNetLib;
using LiteNetLib.Utils;
using Metarca.Server.Systems.Player;
using Metarca.Server.Utilities;
using Metarca.Shared.Packets;

namespace Metarca.Server.Systems;

public class ConnectionSystem : System
{
    public delegate void OnVerifyPeerConnectionData(ConnectionPacket packet, ResettableLatch reject);
    public event OnVerifyPeerConnectionData? VerifyPeerConnectionEvent;
    public delegate void OnPeerConnected(NetPeer peer);
    public event OnPeerConnected? PeerConnectedEvent;
    public delegate void OnPeerDisconnected(NetPeer peer);
    public event OnPeerDisconnected? PeerDisconnectedEvent;
    
    private readonly ConnectionPacket connectionPacket = new();
    private readonly ResettableLatch rejectionLatch = new();

    public ConnectionSystem(Deps deps) : base(deps)
    {
        listener.ConnectionRequestEvent += ConnectionRequest;
        listener.PeerConnectedEvent += PeerConnected;
        listener.PeerDisconnectedEvent += PeerDisconnected;
    }

    private void ConnectionRequest(ConnectionRequest request)
    {
        try
        {
            connectionPacket.Deserialize(request.Data);
        }
        catch (Exception)
        {
            request.Reject();
            Print("Rejected connection request, exception while deserializing connection data packet.");
            return;
        }

        rejectionLatch.Reset();
        VerifyPeerConnectionEvent?.Invoke(connectionPacket, rejectionLatch);

        if (rejectionLatch.IsSet)
        {
            request.Reject();
            Print("Rejected connection request, peer verification failed.");
            return;
        }

        request.Accept();
    }
    private void PeerConnected(NetPeer peer)
    {
        PeerConnectedEvent?.Invoke(peer);
    }
    private void PeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        PeerDisconnectedEvent?.Invoke(peer);
    }
}