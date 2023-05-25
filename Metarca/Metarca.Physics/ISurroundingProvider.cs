namespace Metarca.Physics;

internal interface ISurroundingProvider
{
    public IEnumerable<Entity> GetSurroundingEntities(uint cellId);
}