using LiteNetLib.Utils;
using LiteNetLib;

namespace Metarca.Server.Ecs;

public record Deps
(
    Server Server,
    EventBasedNetListener Listener,
    NetManager NetManager,
    NetPacketProcessor PacketProcessor,
    NetOut NetOut
);