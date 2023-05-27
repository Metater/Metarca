using System.Numerics;

namespace Metarca.Physics.Simulation;

internal static class SimHelper
{
    internal static Vector2 GetForceOnRepulsee(Entity repulsee, Entity repulsor, out bool interacting)
    {
        float distanceX = repulsor.Position.X - repulsee.Position.X;
        float distanceY = repulsor.Position.Y - repulsee.Position.Y;
        float squareDistance = distanceX * distanceX + distanceY * distanceY;
        float minDistance = repulsee.repulsion.radius + repulsor.repulsion.radius;

        interacting = minDistance * minDistance > squareDistance;

        if (interacting)
        {
            float minMaxRepulsionForce = Math.Min(repulsee.repulsion.maxMagnitude, repulsor.repulsion.maxMagnitude);

            if (squareDistance != 0)
            {
                float repulsionMagnitude = (repulsee.repulsion.force + repulsor.repulsion.force) * (1 / squareDistance);
                repulsionMagnitude = Math.Clamp(repulsionMagnitude, -minMaxRepulsionForce, minMaxRepulsionForce);
                float reciprocalDistance = MathF.ReciprocalSqrtEstimate(squareDistance);
                return new Vector2(distanceX * reciprocalDistance, distanceY * reciprocalDistance) * -repulsionMagnitude;
            }
            else
            {
                float randomDirection = Random.Shared.NextSingle() * 2f * MathF.PI;
                return new Vector2(MathF.Cos(randomDirection), MathF.Sin(randomDirection)) * -minMaxRepulsionForce;
            }
        }

        return Vector2.Zero;
    }

    internal static Vector2 GetNextPosition(Entity entity, double deltaTime)
    {
        return entity.Position + (entity.Velocity * (float)deltaTime);
    }

    internal static bool CollidersIntersect(Collider a, Collider b)
    {
        if (a.max.X < b.min.X || a.min.X > b.max.X)
        {
            return false;
        }

        if (a.max.Y < b.min.Y || a.min.Y > b.max.Y)
        {
            return false;
        }

        return true;
    }
}