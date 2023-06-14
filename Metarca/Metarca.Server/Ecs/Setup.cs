namespace Metarca.Server.Ecs;

public abstract class Setup<T>
{
    protected readonly T t;

    public Setup(T t)
    {
        this.t = t;
    }
}