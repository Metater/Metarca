using Metarca.Server.Ecs;

namespace Metarca.Server.Begin;

public class NetPollSys : DepsSys<ServerDeps>
{
    public NetPollSys(ServerDeps deps, Sys parent) : base(deps, parent)
    {

    }

    protected override void Tick()
    {
        deps.netManager.PollEvents();
    }
}
