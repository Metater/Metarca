using LiteNetLib.Utils;
using LiteNetLib;

namespace Metarca.Server.Ecs;

public abstract class System : ITickable
{
    private readonly Server server;
    protected readonly EventBasedNetListener listener;
    protected readonly NetManager netManager;
    protected readonly NetPacketProcessor packetProcessor;
    private readonly NetOut netOut;

    public System(Deps deps)
    {
        server = deps.Server;
        listener = deps.Listener;
        netManager = deps.NetManager;
        packetProcessor = deps.PacketProcessor;
        netOut = deps.NetOut;
    }

    #region Wrappers
    public void SendPacketToPeer<T>(NetPeer peer, T packet, DeliveryMethod deliveryMethod) where T : class, new()
    {
        netOut.SendPacketToPeer(peer, packet, deliveryMethod);
    }
    public void SendPacketToAll<T>(T packet, DeliveryMethod deliveryMethod) where T : class, new()
    {
        netOut.SendPacketToAll(packet, deliveryMethod);
    }
    #endregion

    /// <summary>
    /// Called before Start(), use for wiring system events
    /// </summary>
    public virtual void Awake() { }
    /// <summary>
    /// Called before first tick
    /// </summary>
    public virtual void Start() { }
    /// <summary>
    /// Called every tick
    /// </summary>
    public virtual void Tick() { }
    /// <summary>
    /// Called after last tick
    /// </summary>
    public virtual void Stop() { }
    /// <summary>
    /// Prints a message with a timestamp and references this system
    /// </summary>
    /// <param name="message"></param>
    public void Print(string message)
    {
        Console.WriteLine($"[{DateTime.Now}] [{GetType().Name}] {message}");
    }
}