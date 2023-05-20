namespace Metarca.Server;

public interface ITickable
{
    public void Tick(double time, ulong tickId);
}