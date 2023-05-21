namespace Metarca.Server.Physics;

public interface ISteppable
{
    public void Step(double time, double deltaTime);
}