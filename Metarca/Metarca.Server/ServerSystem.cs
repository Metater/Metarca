namespace Metarca.Server;

public abstract class ServerSystem
{
    public virtual bool VerifyPeerConnectionData() => true;
}