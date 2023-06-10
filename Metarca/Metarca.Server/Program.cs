using LiteNetLib;
using LiteNetLib.Utils;
using Metarca.Server;
using Metarca.Server.Ecs;
using Metarca.Shared;

// Reminders:
// The point of structuring systems should only be execution order control, not arbitrary organization

Console.WriteLine("Hello, World!");

CancellationTokenSource cts = new();
Console.CancelKeyPress += (s, e) =>
{
    cts.Cancel();
    e.Cancel = true;
};

EventBasedNetListener listener = new();
NetManager netManager = new(listener)
{
    AutoRecycle = true
};
NetPacketProcessor packetProcessor = new();
NetOut netOut = new(netManager, packetProcessor);

DepsSys.Deps deps = new(listener, netManager, packetProcessor, netOut);

RootSys root = new(deps);

root.InvokeCompose();
root.InvokeAwake();
root.InvokeStart();

Time.Start();

while (!cts.IsCancellationRequested)
{
    // Tick until caught up
    while (Time.ShouldTick())
    {
        root.InvokeTick();

        if (Time.TickId % Constants.TicksPerSecond == 0)
        {
            Console.Title = $"TPS: {Constants.TicksPerSecond} | Uptime: {(ulong)Time.Now}s | Tick Id: {Time.TickId} | Time Per Tick: {(Time.Now - Time.TickTime) * 1000.0:0.000}ms";
        }
    }

    Thread.Sleep(1);
}

root.InvokeStop();