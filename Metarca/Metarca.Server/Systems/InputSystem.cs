using LiteNetLib;
using Metarca.Server.Extensions;
using Metarca.Shared.Packets;

namespace Metarca.Server.Systems;

public class InputSystem : System
{
    public InputSystem(Deps deps) : base(deps)
    {
        packetProcessor.RegisterNestedType<InputPacket.Data>();
        packetProcessor.SubscribeReusable<InputPacket, NetPeer>(OnInputPacket);
    }

    private void OnInputPacket(InputPacket packet, NetPeer peer)
    {
        // peer.GetPlayerData()
    }
}