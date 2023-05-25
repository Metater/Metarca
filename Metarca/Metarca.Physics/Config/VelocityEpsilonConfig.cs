namespace Metarca.Physics.Config;

public struct VelocityEpsilonConfig
{
    public bool enabled;
    public float epsilon;

    public VelocityEpsilonConfig()
    {
        enabled = false;
        epsilon = 0;
    }

    public VelocityEpsilonConfig(float epsilon)
    {
        enabled = true;
        this.epsilon = epsilon;
    }
}