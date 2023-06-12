using Metarca.Server.Ecs;
using Metarca.Shared;

namespace Metarca.Server.Start;

public class NetStartSys : DepsSys<ServerDeps>
{
    public NetStartSys(ServerDeps deps, Sys parent) : base(deps, parent)
    {
        
    }

    protected override void Start()
    {
        deps.netManager.Start(Constants.ServerPort);
    }
}