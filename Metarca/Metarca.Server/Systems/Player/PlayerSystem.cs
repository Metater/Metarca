using LiteNetLib;
using Metarca.Server.Extensions;
using Metarca.Server.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metarca.Server.Systems.Player;

public class PlayerSystem : System
{
    private readonly List<PlayerEntity> players = new();

    public PlayerSystem(Deps deps) : base(deps)
    {

    }

    public override void Awake()
    {
        Get<ConnectionSystem>().PeerConnectedEvent += PeerConnected;
        Get<ConnectionSystem>().PeerDisconnectedEvent += PeerDisconnected;
    }

    public override void Tick()
    {

    }

    private void PeerConnected(NetPeer peer)
    {
        PlayerData data = peer.GetPlayerData();
        players.Add(data.Entity);
    }
    private void PeerDisconnected(NetPeer peer)
    {
        PlayerData data = peer.GetPlayerData();
        players.Remove(data.Entity);
    }
}