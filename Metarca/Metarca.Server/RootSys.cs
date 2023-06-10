using Metarca.Server.Connection;
using Metarca.Server.Ecs;

namespace Metarca.Server;

public class RootSys : NetSys
{
    public RootSys(Deps deps) : base(deps)
    {

    }

    protected override void Compose()
    {
        AddSub(new ConnectionSys(deps, this));
        AddSub(new NetInSys(deps, this));
        AddSub(new TimeSyncSys(deps, this));


    }
    protected override void Awake()
    {

    }
    protected override void Start()
    {
        netManager.Start(7777);
    }
    protected override void Tick()
    {
        netManager.PollEvents();


    }
    protected override void Stop()
    {

    }
}