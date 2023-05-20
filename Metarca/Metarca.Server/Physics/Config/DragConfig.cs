namespace Metarca.Server.Physics.Config;

public struct DragConfig
{
    public bool enabled;
    public float force;

    public DragConfig()
    {
        enabled = false;
        force = 0;
    }

    public DragConfig(float force)
    {
        enabled = true;
        this.force = force;
    }
}