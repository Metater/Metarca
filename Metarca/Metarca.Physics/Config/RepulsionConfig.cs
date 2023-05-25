namespace Metarca.Physics.Config;

public struct RepulsionConfig
{
    public bool canRepulseOthers;
    public bool canBeRepulsed;
    public float radius;
    public float maxMagnitude;
    public float force;

    public RepulsionConfig()
    {
        canRepulseOthers = false;
        canBeRepulsed = false;
        radius = 0;
        maxMagnitude = 0;
        force = 0;
    }

    public RepulsionConfig(bool canRepulseOthers, bool canBeRepulsed, float radius, float maxMagnitude, float force)
    {
        this.canRepulseOthers = canRepulseOthers;
        this.canBeRepulsed = canBeRepulsed;
        this.radius = radius;
        this.maxMagnitude = maxMagnitude;
        this.force = force;
    }
}