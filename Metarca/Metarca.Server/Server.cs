using Metarca.Shared;
using LiteNetLib;
using Metarca.Server.Physics;
using Metarca.Shared.Packets;
using LiteNetLib.Utils;
using Metarca.Server.Systems;
using Metarca.Server.Systems.Player;
using Metarca.Server.Ecs;

namespace Metarca.Server;

public class Server : ITickable
{
    private readonly EventBasedNetListener listener = new();
    private readonly NetManager netManager;
    private readonly NetPacketProcessor packetProcessor = new();
    private readonly NetOut netOut;

    private readonly Zone zone = new();

    private readonly List<System> systems = new();
    private readonly Dictionary<Type, System> systemsDictionary = new();

    public Server()
    {
        netManager = new(listener)
        {
            AutoRecycle = true
        };

        netOut = new(netManager, packetProcessor);

        // Initialize systems
        System.Deps deps = new(this, listener, netManager, packetProcessor, netOut);

        Add(new ConnectionSystem(deps));
        Add(new NetInSystem(deps));
        Add(new TimeSyncSystem(deps));

        Add(new InputSystem(deps));

        Add(new WorldSystem(deps));

        Add(new PlayerInitSystem(deps));
        Add(new PlayerCleanupSystem(deps));
        Add(new PlayerSystem(deps));
    }

    public void Start()
    {
        netManager.Start(7777);

        foreach (var system in systems)
        {
            system.Awake();
        }

        foreach (var system in systems)
        {
            system.Start();
        }
    }

    public void Tick()
    {
        netManager.PollEvents();

        zone.Step(Time.TickTime, Constants.SecondsPerTick);
        zone.Tick();

        foreach (var system in systems)
        {
            system.Tick();
        }

        if (Time.TickId % Constants.TicksPerSecond == 0)
        {
            Console.Title = $"TPS: {Constants.TicksPerSecond} | Uptime: {(ulong)Time.Now}s | Tick Id: {Time.TickId} | Time Per Tick: {(Time.Now - Time.TickTime) * 1000.0:0.000}ms";
        }
    }

    public void Stop()
    {
        netManager.Stop();

        foreach (var system in systems)
        {
            system.Stop();
        }
    }

    #region System Lifetime
    private void Add<T>(T system) where T : System
    {
        systems.Add(system);
        Type type = system.GetType();
        systemsDictionary.Add(type, system);
        Console.WriteLine($"Initialized \"{type.Name}\"");
    }
    public T Get<T>() where T : System
    {
        return (T)systemsDictionary[typeof(T)];
    }
    #endregion
}
