using System.Numerics;
using Metarca.Server.Ecs;

namespace Metarca.Server.Physics;

public class Zone : ISteppable, ITickable
{
    private const uint CellSize = 16;
    private const uint HalfCellSize = CellSize / 2;
    private const uint RowLength = (uint)ushort.MaxValue + 1;
    private const uint HalfRowLength = RowLength / 2;
    private const uint RowCellLength = RowLength / CellSize;
    private const uint HalfRowCellLength = RowCellLength / 2;

    private readonly List<Entity> entities = new();
    // <cellIndex, entities>
    private readonly Dictionary<uint, List<Entity>> cells = new();
    private readonly List<Entity>?[] nearbyCells = new List<Entity>?[9];

    public void Step(double time, double deltaTime)
    {
        foreach (var entity in entities)
        {
            entity.Step(time, deltaTime);
        }
    }

    public void Tick()
    {
        foreach (var entity in entities)
        {
            entity.Tick();
        }
    }

    #region Entity Lifetime
    // NOT PUBLIC API
    public void SpawnEntity(Entity entity)
    {
        entities.Add(entity);
    }
    // NOT PUBLIC API
    public void DespawnEntity(Entity entity)
    {
        cells[entity.OccupiedCellIndex].Remove(entity);
        entities.Remove(entity);
    }
    // NOT PUBLIC API
    public void AddEntity(Entity entity, uint cellIndex)
    {
        if (!cells.TryGetValue(cellIndex, out var cell))
        {
            cell = new();
            cells[cellIndex] = cell;
        }

        cell.Add(entity);
    }
    // NOT PUBLIC API
    public void RemoveEntity(Entity entity, uint cellIndex)
    {
        List<Entity> cell = cells[cellIndex];
        cell.Remove(entity);

        if (cell.Count == 0)
        {
            cells.Remove(cellIndex);
        }
    }
    #endregion

    #region Nearby
    public IEnumerator<Entity> GetNearbyRepulsors(uint cellIndex)
    {
        PopulateNearbyCells(cellIndex);

        foreach (var cell in nearbyCells)
        {
            if (cell == null) continue;

            foreach (var entity in cell)
            {
                if (!entity.Repulsion.canRepulseOthers) continue;

                yield return entity;
            }
        }
    }
    public IEnumerator<Entity> GetNearbyColliders(uint cellIndex)
    {
        PopulateNearbyCells(cellIndex);

        foreach (var cell in nearbyCells)
        {
            if (cell == null) continue;

            foreach (var entity in cell)
            {
                if (!entity.Collider.enabled) continue;

                yield return entity;
            }
        }
    }
    private void PopulateNearbyCells(uint cellIndex)
    {
        PopulateNearbyCell(0, cellIndex);
        PopulateNearbyCell(1, cellIndex - RowCellLength);
        PopulateNearbyCell(2, cellIndex + RowCellLength);
        PopulateNearbyCell(3, cellIndex + 1);
        PopulateNearbyCell(4, cellIndex - 1);
        PopulateNearbyCell(5, cellIndex - RowCellLength + 1);
        PopulateNearbyCell(6, cellIndex - RowCellLength - 1);
        PopulateNearbyCell(7, cellIndex + RowCellLength + 1);
        PopulateNearbyCell(8, cellIndex + RowCellLength - 1);
    }
    private void PopulateNearbyCell(int index, uint cellIndex)
    {
        cells.TryGetValue(cellIndex, out var cell);
        nearbyCells[index] = cell;
    }
    #endregion

    #region Utility
    // x: [-524288, 524272], y: [-524288, 524272] -> [0, 4294967295]
    public static uint GetCellIndex(Vector2 position)
    {
        return GetCellIndex(GetCellCoord(position.X), GetCellCoord(position.Y));
    }
    // x: [0, 65535], y: [0, 65535] -> [0, 4294967295]
    private static uint GetCellIndex(ushort x, ushort y)
    {
        return (RowLength * y) + x;
    }
    // index: [0, 4294967295] -> (x: [0, 65535], y: [0, 65535])
    private static (ushort x, ushort y) GetCellCoords(uint index)
    {
        return ((ushort)(index % RowLength), (ushort)(index / RowLength));
    }
    // x: [-524288, 524272], y: [-524288, 524272] -> (x: [0, 65535], y: [0, 65535])
    private static (ushort x, ushort y) GetCellCoords(Vector2 position)
    {
        return new(GetCellCoord(position.X), GetCellCoord(position.Y));
    }
    // position: [-524288, 524272] -> [0, 65535]
    private static ushort GetCellCoord(float position)
    {
        return (ushort)MathF.Round((position / CellSize) + HalfRowLength);
    }
    // index: [0, 4294967295] -> (x: [-524288 + 8, 524272 + 8], y: [-524288 + 8, 524272 + 8])
    private static Vector2 GetZoneCoordsCenter(uint index)
    {
        Vector2 zoneCoords = GetZoneCoords(index);
        return new(zoneCoords.X + HalfCellSize, zoneCoords.Y + HalfCellSize);
    }
    // index: [0, 4294967295] -> (x: [-524288, 524272], y: [-524288, 524272])
    private static Vector2 GetZoneCoords(uint index)
    {
        float x = (((float)index % RowCellLength) - HalfRowCellLength) * CellSize;
        float y = (((float)index / RowCellLength) - HalfRowCellLength) * CellSize;
        return new(x, y);
    }
    #endregion
}