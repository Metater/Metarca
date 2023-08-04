using LiteNetLib;
using LiteNetLib.Utils;
using Metarca.Server;
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

Time time = new();

EventBasedNetListener listener = new();
NetManager netManager = new(listener)
{
    AutoRecycle = true
};
NetPacketProcessor packetProcessor = new();
NetOut netOut = new(netManager, packetProcessor);

ServerDeps deps = new()
{
    Time = time,
    Listener = listener,
    NetManager = netManager,
    PacketProcessor = packetProcessor,
    NetOut = netOut
};

RootSys root = new(deps);

root.InvokeCompose();
root.InvokeStart();

time.Start();

while (!cts.IsCancellationRequested)
{
    // Tick until caught up
    while (time.ShouldTick())
    {
        root.InvokeTick();

        if (time.TickId % Constants.TicksPerSecond == 0)
        {
            Console.Title = $"TPS: {Constants.TicksPerSecond} | Uptime: {(ulong)time.Now}s | Tick Id: {time.TickId} | Time Per Tick: {(time.Now - time.TickTime) * 1000.0:0.000}ms";
        }
    }

    Thread.Sleep(1);
}

root.InvokeStop();