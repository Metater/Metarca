using Metarca.Server.Ecs;

namespace Metarca.Server;

public class BeginSys : DepsSys<ServerDeps>
{
    public BeginSys(ServerDeps deps, Sys? parent = null) : base(deps, parent)
    {

    }
}