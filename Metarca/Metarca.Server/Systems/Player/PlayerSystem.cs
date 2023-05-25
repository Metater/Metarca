using LiteNetLib;
using Metarca.Server.Extensions;
using Metarca.Server.Physics;
using Metarca.Shared;
using Metarca.Shared.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Metarca.Server.Systems.Player.PlayerCleanupSystem;

namespace Metarca.Server.Systems.Player;

public class PlayerSystem : System
{
    private readonly List<PlayerEntity> players = new();

    public PlayerSystem(Deps deps) : base(deps)
    {
        packetProcessor.RegisterNestedType<DebugEntity>();
    }

    public override void Awake()
    {
        Get<PlayerInitSystem>().PlayerJoinedEvent += PlayerJoined;
        Get<PlayerCleanupSystem>().PlayerLeftEvent += PlayerLeft;
    }

    public override void Tick()
    {
        DebugEntity[] entities = new DebugEntity[players.Count];

        for (int i = 0; i < players.Count; i++)
        {
            Vector2 position = players[i].Position;
            entities[i] = new()
            {
                Id = (byte)i,
                X = position.X,
                Y = position.Y
            };
        }

        SendPacketToAll(new DebugEntityPacket
        {
            Entities = entities.ToArray()
        }, DeliveryMethod.Unreliable);
    }

    private void PlayerJoined(PlayerData data) => players.Add(data.Entity);
    private void PlayerLeft(PlayerData data) => players.Remove(data.Entity);
}