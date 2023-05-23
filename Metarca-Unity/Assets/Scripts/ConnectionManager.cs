using LiteNetLib;
using LiteNetLib.Utils;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private string address;
    [SerializeField] private int port;
    private readonly NetDataWriter connectionData = new();

    private void Start()
    {
        ClientManager.PeerDisconnectedEvent += PeerDisconnectedEvent;

        Connect();
    }

    private void PeerDisconnectedEvent(DisconnectInfo disconnectInfo) => Connect();

    private void Connect() => ClientManager.NetManager.Connect(address, port, connectionData);
}
