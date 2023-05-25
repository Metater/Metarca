using System.Numerics;

namespace Metarca.Physics;

public struct Collider
{
    public Vector2 min;
    public Vector2 max;

    public float North => max.Y;
    public float East => max.X;
    public float South => min.Y;
    public float West => min.X;

    public static Collider All => new(new(float.MinValue, float.MinValue), new(float.MaxValue, float.MaxValue));

    public Collider()
    {
        min = new();
        max = new();
    }

    public Collider(Vector2 min, Vector2 max)
    {
        this.min = min;
        this.max = max;
    }

    public Collider Offset(Vector2 offset)
    {
        return new Collider(min + offset, max + offset);
    }

    public bool Intersects(Collider other)
    {
        if (max.X < other.min.X || min.X > other.max.X)
        {
            return false;
        }
        if (max.Y < other.min.Y || min.Y > other.max.Y)
        {
            return false;
        }
        return true;
    }
}