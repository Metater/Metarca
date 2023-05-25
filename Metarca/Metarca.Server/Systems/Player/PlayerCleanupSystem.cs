using LiteNetLib;
using Metarca.Server.Extensions;

namespace Metarca.Server.Systems.Player;

public class PlayerCleanupSystem : System
{
    public delegate void OnPlayerLeft(PlayerData data);
    public event OnPlayerLeft? PlayerLeftEvent;

    public PlayerCleanupSystem(Deps deps) : base(deps)
    {
        Get<ConnectionSystem>().PeerDisconnectedEvent += PeerDisconnected;
    }

    private void PeerDisconnected(NetPeer peer)
    {
        PlayerData data = peer.GetPlayerData();
        PlayerLeftEvent?.Invoke(data);
        DeconstructPlayerData(data);
    }

    #region Deconstruction
    private static void DeconstructPlayerData(PlayerData data)
    {
        data.Entity.Despawn();
    }
    #endregion
}