namespace Metarca.Shared.Packets;

public class PeriodicNotifyClientPacket : IPoolable
{
    public static int SizeOf = 8;

    public double Time { get; set; }
}