using Metarca.Shared;
using System.Diagnostics;

namespace Metarca.Server;

public static class Time
{
    private static readonly Stopwatch stopwatch = new();
    private static ulong nextTickId = 0;

    public static double Now => stopwatch.Elapsed.TotalSeconds;
    public static double TickTime { get; private set; }
    public static ulong TickId { get; private set; }

    public static void Start()
    {
        stopwatch.Start();
    }

    public static bool ShouldTick()
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