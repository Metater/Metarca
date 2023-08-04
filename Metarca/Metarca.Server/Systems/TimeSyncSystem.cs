using LiteNetLib;
using Metarca.Shared.Packets;

namespace Metarca.Server.Systems;

public class TimeSyncSystem : System
{
    public TimeSyncSystem(Deps deps) : base(deps)
    {
        
    }

    public override void Tick()
    {
        SendPacketToAll(new TimePacket()
        {
            Time = Time.TickTime
        }, DeliveryMethod.Unreliable);
    }
}