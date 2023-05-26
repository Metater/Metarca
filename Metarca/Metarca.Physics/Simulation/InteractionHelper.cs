namespace Metarca.Physics.Simulation;

internal static class InteractionHelper
{
    internal static bool ShouldInteract(Entity a, Entity b)
    {
        return (a.LayerMask & b.LayerMask) != 0;
    }
}