using LiteNetLib;
using Metarca.Server.Ecs;
using Metarca.Shared.Packets;

namespace Metarca.Server;

public class TimeSyncSys : NetSys
{
    public TimeSyncSys(Deps deps, Sys? parent = null) : base(deps, parent)
    {

    }

    protected override void Tick()
    {
        SendPacketToAll(new PeriodicNotifyClientPacket()
        {
            Time = Time.TickTime
        }, DeliveryMethod.Unreliable);
    }
}