using LiteNetLib.Utils;
using LiteNetLib;
using Metarca.Shared.Collections;

namespace Metarca.Server;

public class ServerDeps
{
    public readonly GenericQueues queues = new();
    public readonly GenericEvents events = new();
    public readonly GenericPools pools = new(100);

    public readonly Time time;
    public readonly EventBasedNetListener listener;
    public readonly NetManager netManager;
    public readonly NetPacketProcessor packetProcessor;
    public readonly NetOut netOut;

    public ServerDeps(Time time, EventBasedNetListener listener, NetManager netManager, NetPacketProcessor packetProcessor, NetOut netOut)
    {
        this.time = time;
        this.listener = listener;
        this.netManager = netManager;
        this.packetProcessor = packetProcessor;
        this.netOut = netOut;
    }
}