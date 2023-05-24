namespace Metarca.Server.Utilities;

public class ResettableLatch
{
    public bool IsSet { get; private set; }

    public void Set() => IsSet = true;
    public void Reset() => IsSet = false;
}