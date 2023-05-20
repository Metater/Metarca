using Metarca.Server.Physics.Types;

namespace Metarca.Server.Physics.Config;

public struct ColliderConfig
{
    public bool enabled;
    public Collider collider;

    public ColliderConfig()
    {
        enabled = false;
        collider = new();
    }

    public ColliderConfig(Collider collider)
    {
        enabled = true;
        this.collider = collider;
    }
}