namespace Metarca.Server.Interfaces;

public interface ITickable
{
    public void Tick(double time, ulong tickId);
}