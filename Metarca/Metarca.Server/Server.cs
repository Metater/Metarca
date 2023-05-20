using Metarca.Shared;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metarca.Server;

public class Server
{
    private readonly ServerManager serverManager = new();

    public Server()
    {
        serverManager.netManager.Start(7777);

        serverManager.packetProcessor.RegisterNestedType<DebugEntity>();
    }

    public void PollEvents()
    {
        serverManager.netManager.PollEvents();
    }

    public void Tick(double time, ulong tickId)
    {
        if (tickId % Constants.TicksPerSecond == 0)
        {
            // Runs once per second
        }

        serverManager.SendPacketToAll(new TimePacket()
        {
            Time = time
        }, DeliveryMethod.Unreliable);

        if (time > 10)
        {
            var debugEntity = new DebugEntity
            {
                Id = 0,
                X = (float)Math.Cos(time * 4) * 8, // (float)Math.Cos(time * 4) * 8, // tickId % 40 < 20 ? -8 : 8
                Y = 0
            };

            serverManager.SendPacketToAll(new DebugEntityPacket()
            {
                Entities = new DebugEntity[] { debugEntity }
            }, DeliveryMethod.Unreliable);
        }

        Console.Title = $"Tick Id: {tickId}, TPS: {Constants.TicksPerSecond}";
    }

    public void Stop()
    {
        serverManager.netManager.Stop();
    }
}
