using LiteNetLib;

namespace Metarca.Server.Extensions;

public static class NetPeerExtensions
{
    public static PlayerData GetPlayerData(this NetPeer peer)
    {
        return (PlayerData)peer.Tag;
    }
}