namespace Metarca.Server.Ecs;

public abstract class DepsSys<TDeps> : Sys
{
    protected TDeps deps;

    public DepsSys(TDeps deps, Sys? parent = null) : base(parent)
    {
        this.deps = deps;
    }
}