namespace Metarca.Server.Physics.Types;

public interface ISteppable
{
    public void Step(double time, double deltaTime);
}