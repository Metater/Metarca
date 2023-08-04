using LiteNetLib.Utils;
using LiteNetLib;
using Metarca.Shared.Collections;

namespace Metarca.Server;

public class ServerDeps
{
    /// <summary>
    /// Nust be handled by the end of the frame
    /// </summary>
    public readonly IEphemeralQueues ephemeralQueues = new Queues();
    /// <summary>
    /// Must be handled manually
    /// </summary>
    public readonly IManualQueues manualQueues = new Queues();
    /// <summary>
    /// Cleared at the end of the frame
    /// </summary>
    public readonly IEphemeralEvents ephemeralEvents = new Events();
    /// <summary>
    /// Must be handled manually
    /// </summary>
    public readonly IManualEvents manualEvents = new Events();
    public readonly Pools pools = new(100);
    public readonly Singletons singletons = new();

    public required Time Time { get; init; }
    public required EventBasedNetListener Listener { get; init; }
    public required NetManager NetManager { get; init; }
    public required NetPacketProcessor PacketProcessor { get; init; }
    public required NetOut NetOut { get; init; }
}