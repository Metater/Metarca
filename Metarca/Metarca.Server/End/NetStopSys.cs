using Metarca.Server.Ecs;

namespace Metarca.Server.End;

public class NetStopSys : DepsSys<ServerDeps>
{
    public NetStopSys(ServerDeps deps, Sys parent) : base(deps, parent)
    {

    }

    protected override void Stop()
    {
        deps.netManager.Stop();
    }
}