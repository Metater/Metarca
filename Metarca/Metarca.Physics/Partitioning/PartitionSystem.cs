namespace Metarca.Physics.Partitioning;

internal class PartitionSystem : ISurroundingProvider
{
    private static readonly (int x, int y)[] SurroundingOffsets = { (0, 0), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1) };
    private readonly Dictionary<uint, List<Entity>> cells = new();

    internal void EnterCell(uint cellId, Entity entity)
    {
        if (!cells.TryGetValue(cellId, out var cell))
        {
            cell = new();
            cells[cellId] = cell;
        }

        cell.Add(entity);
    }
    internal bool ExitCell(uint cellId, Entity entity)
    {
        if (cells.TryGetValue(cellId, out var cell))
        {
            return cell.Remove(entity);
        }

        return false;
    }

    public IEnumerable<Entity> GetSurroundingEntities(uint cellId)
    {
        (ushort x, ushort y) = PartitionHelper.GetCoords(cellId);

        foreach ((int offsetX, int offsetY) in SurroundingOffsets)
        {
            foreach (var entity in GetEntitiesAtCoords((ushort)(x + offsetX), (ushort)(y + offsetY)))
            {
                yield return entity;
            }
        }
    }

    private IEnumerable<Entity> GetEntitiesAtCoords(ushort x, ushort y)
    {
        uint cellId = PartitionHelper.GetCellId(x, y);

        if (!cells.TryGetValue(cellId, out var cell))
        {
            yield break;
        }

        foreach (var entity in cell)
        {
            yield return entity;
        }
    }

    // IEnumerator<Entity> GetEntitiesInBounds()
    // IEnumerator<Entity> GetEntitiesInRadius()
}
