using LiteNetLib;
using Metarca.Server.Ecs;

namespace Metarca.Server.Setup;

public class NetListenerSetup : Setup<ServerDeps>
{
    public NetListenerSetup(ServerDeps t) : base(t)
    {
        t.Listener.ConnectionRequestEvent += Listener_ConnectionRequestEvent;
    }

    private void Listener_ConnectionRequestEvent(ConnectionRequest request)
    {
        request.Accept();
    }
}