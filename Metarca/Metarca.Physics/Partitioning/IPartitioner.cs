using System.Numerics;

namespace Metarca.Physics.Partitioning;

public interface IPartitioner
{
    public uint GetCellId(Vector2 position);
    public uint GetCellId(ushort x, ushort y);
    public (ushort x, ushort y) GetCoords(uint cellId);
    public (ushort x, ushort y) GetCoords(Vector2 position);
    public ushort GetCoord(float position);
    public Vector2 GetCenterPosition(uint cellId);
    public Vector2 GetPosition(uint cellId);
}