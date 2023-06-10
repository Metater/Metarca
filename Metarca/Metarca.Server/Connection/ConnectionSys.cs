using LiteNetLib;
using Metarca.Server.Ecs;
using Metarca.Shared.Packets;

namespace Metarca.Server.Connection;

public class ConnectionSys : DepsSys
{
    public delegate void OnVerifyPeerConnectionData(ConnectionDataPacket packet, Rejector rejector);
    public event OnVerifyPeerConnectionData? VerifyPeerConnectionEvent;
    public delegate void OnPeerConnected(NetPeer peer);
    public event OnPeerConnected? PeerConnectedEvent;
    public delegate void OnPeerDisconnected(NetPeer peer);
    public event OnPeerDisconnected? PeerDisconnectedEvent;

    private readonly ConnectionDataPacket connectionPacket = new();
    private readonly Rejector rejector = new();

    public ConnectionSys(Deps deps, Sys? parent = null) : base(deps, parent)
    {

    }

    protected override void Awake()
    {
        listener.ConnectionRequestEvent += Listener_ConnectionRequestEvent;
        listener.PeerConnectedEvent += Listener_PeerConnectedEvent;
        listener.PeerDisconnectedEvent += Listener_PeerDisconnectedEvent;
    }

    private void Listener_ConnectionRequestEvent(ConnectionRequest request)
    {
        try
        {
            connectionPacket.Deserialize(request.Data);
        }
        catch (Exception)
        {
            request.Reject();
            Print(
                "rejected connection request",
                "exception while deserializing connection data packet"
            );
            return;
        }

        rejector.Reset();
        VerifyPeerConnectionEvent?.Invoke(connectionPacket, rejector);

        if (rejector.ShouldReject)
        {
            request.Reject();
            Print(
                "rejected connection request",
                "peer verification failed"
            );
            return;
        }

        request.Accept();
    }

    private void Listener_PeerConnectedEvent(NetPeer peer)
        => PeerConnectedEvent?.Invoke(peer);
    private void Listener_PeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
        => PeerDisconnectedEvent?.Invoke(peer);
}
