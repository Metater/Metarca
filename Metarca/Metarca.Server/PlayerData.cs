using LiteNetLib;
using Metarca.Server.Systems.Player;

namespace Metarca.Server;

public class PlayerData
{
    public required NetPeer Peer { get; init; }
    public required PlayerEntity Entity { get; init; }
}