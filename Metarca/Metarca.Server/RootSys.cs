using Metarca.Server.Connection;
using Metarca.Server.Ecs;
using Metarca.Shared;

namespace Metarca.Server;

public class RootSys : DepsSys
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
        netManager.Start(Constants.ServerPort);
    }
    protected override void Tick()
    {
        netManager.PollEvents();
    }
    protected override void Stop()
    {

    }
}