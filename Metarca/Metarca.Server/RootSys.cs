using Metarca.Server.Connection;
using Metarca.Server.Ecs;
using Metarca.Shared;

namespace Metarca.Server;

public class RootSys : DepsSys<ServerDeps>
{
    public RootSys(ServerDeps deps) : base(deps)
    {

    }

    protected override void Compose()
    {
        AddSub(new ConnectionSys(deps, this));
        AddSub(new NetInSys(this));
        AddSub(new TimeSyncSys(this));


    }
    protected override void Start()
    {
        
    }
    protected override void Tick()
    {
        
    }
    protected override void Stop()
    {
        
    }
}