using Metarca.Physics.Config;
using Metarca.Physics.Public;
using System.Numerics;

namespace Metarca.Physics;

public abstract class Entity
{
    private const float DefaultVelocityEpsilon = 1f / 256f;

    public ColliderConfig collider = new();
    public BoundsConfig bounds = new();
    public DragConfig drag = new();
    public RepulsionConfig repulsion = new();
    public VelocityEpsilonConfig velocityEpsilon = new(DefaultVelocityEpsilon);

    public uint? CellId { get; internal set; }
    public Vector2 Position { get; internal set; }
    public Vector2 Velocity { get; set; }
    public ulong LayerMask { get; set; } = ulong.MaxValue; // Usage: if self.LayerMask & other.LayerMask != 0, then they should interact

    public Entity(Vector2 position, Vector2 velocity)
    {
        Position = position;
        Velocity = velocity;
    }
}