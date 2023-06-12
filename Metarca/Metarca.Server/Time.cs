using System.Diagnostics;
using Metarca.Shared;

namespace Metarca.Server;

public class Time
{
    private readonly Stopwatch stopwatch = new();
    private ulong nextTickId = 0;

    public double Now => stopwatch.Elapsed.TotalSeconds;
    public double TickTime { get; private set; }
    public ulong TickId { get; private set; }
    
    public void Start()
    {
        stopwatch.Start();
    }

    public bool ShouldTick()
    {
        bool shouldTick = Now * Constants.TicksPerSecond > nextTickId;

        if (shouldTick)
        {
            TickTime = Now;
            TickId = nextTickId++;
        }

        return shouldTick;
    }
}