using System.Numerics;

namespace Metarca.Physics.Partitioning;

internal static class PartitionHelper
{
    private const uint CellSize = 16;
    private const uint HalfCellSize = CellSize / 2;
    private const uint RowLength = (uint)ushort.MaxValue + 1;
    private const uint HalfRowLength = RowLength / 2;
    private const uint RowCellLength = RowLength / CellSize;
    private const uint HalfRowCellLength = RowCellLength / 2;

    // x: [-524288, 524272], y: [-524288, 524272] -> [0, 4294967295]
    internal static uint GetCellId(Vector2 position)
    {
        return GetCellId(GetCoord(position.X), GetCoord(position.Y));
    }
    // x: [0, 65535], y: [0, 65535] -> [0, 4294967295]
    internal static uint GetCellId(ushort x, ushort y)
    {
        return RowLength * y + x;
    }
    // cellId: [0, 4294967295] -> (x: [0, 65535], y: [0, 65535])
    internal static(ushort x, ushort y) GetCoords(uint cellId)
    {
        return ((ushort)(cellId % RowLength), (ushort)(cellId / RowLength));
    }
    // x: [-524288, 524272], y: [-524288, 524272] -> (x: [0, 65535], y: [0, 65535])
    internal static(ushort x, ushort y) GetCoords(Vector2 position)
    {
        return new(GetCoord(position.X), GetCoord(position.Y));
    }
    // position: [-524288, 524272] -> [0, 65535]
    internal static ushort GetCoord(float position)
    {
        return (ushort)MathF.Round(position / CellSize + HalfRowLength);
    }
    // cellId: [0, 4294967295] -> (x: [-524288 + 8, 524272 + 8], y: [-524288 + 8, 524272 + 8])
    internal static Vector2 GetCenterPosition(uint cellId)
    {
        Vector2 position = GetPosition(cellId);
        return new(position.X + HalfCellSize, position.Y + HalfCellSize);
    }
    // cellId: [0, 4294967295] -> (x: [-524288, 524272], y: [-524288, 524272])
    internal static Vector2 GetPosition(uint cellId)
    {
        float x = ((float)cellId % RowCellLength - HalfRowCellLength) * CellSize;
        float y = ((float)cellId / RowCellLength - HalfRowCellLength) * CellSize;
        return new(x, y);
    }
}