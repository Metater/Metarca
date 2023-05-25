using LiteNetLib;
using Metarca.Server.Physics;
using Metarca.Server.Physics.Config;
using System.Numerics;

namespace Metarca.Server.Systems.Player;

public class PlayerInitSystem : System
{
    public delegate void OnPlayerJoined(PlayerData data);
    public event OnPlayerJoined? PlayerJoinedEvent;

    public PlayerInitSystem(Deps deps) : base(deps)
    {
        Get<ConnectionSystem>().PeerConnectedEvent += PeerConnected;
    }

    private void PeerConnected(NetPeer peer)
    {
        PlayerData data = ConstructPlayerData(peer);
        peer.Tag = data;
        PlayerJoinedEvent?.Invoke(data);
    }

    #region Construction
    private PlayerData ConstructPlayerData(NetPeer peer)
    {
        return new()
        {
            Peer = peer,
            Entity = ConstructPlayerEntity()
        };
    }
    private PlayerEntity ConstructPlayerEntity()
    {
        Zone zone = Get<WorldSystem>().zone;
        PlayerEntity entity = new(zone, Vector2.Zero, Vector2.Zero);
        entity
            .WithBounds(new ColliderConfig(new(new(-5, -5), new(5, 5))))
            .WithDrag(new DragConfig(5))
            .WithRepulsion(new RepulsionConfig(true, true, 0.4f, 48, 3));
        return entity;
    }
    #endregion
}
