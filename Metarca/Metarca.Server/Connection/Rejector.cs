namespace Metarca.Server.Connection;

public class Rejector
{
    public bool ShouldReject { get; private set; }

    public void Reject() => ShouldReject = true;
    public void Reset() => ShouldReject = false;
}