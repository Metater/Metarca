using System.Numerics;

namespace Metarca.Physics.Simulation;

internal static class RepulsionMath
{
    internal static Vector2 GetForceOnSelf(Entity self, Entity other)
    {
        float distanceX = other.Position.X - self.Position.X;
        float distanceY = other.Position.Y - self.Position.Y;
        float squareDistance = distanceX * distanceX + distanceY * distanceY;
        float minDistance = self.repulsion.radius + other.repulsion.radius;

        if (minDistance * minDistance > squareDistance)
        {
            float minMaxRepulsionForce = Math.Min(self.repulsion.maxMagnitude, other.repulsion.maxMagnitude);

            if (squareDistance != 0)
            {
                float repulsionMagnitude = (self.repulsion.force + other.repulsion.force) * (1 / squareDistance);
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
}