using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Metarca.Physics.Simulation;

public static class RepulsionUtility
{
    private Vector2 GetRepulsionForce(Entity Entity repulsor)
    {
        float distanceX = repulsor.Position.X - Position.X;
        float distanceY = repulsor.Position.Y - Position.Y;
        float squareDistance = distanceX * distanceX + distanceY * distanceY;
        float minDistance = repulsion.radius + repulsor.repulsion.radius;

        if (minDistance * minDistance > squareDistance)
        {
            float minMaxRepulsionForce = Math.Min(repulsion.maxMagnitude, repulsor.repulsion.maxMagnitude);

            if (squareDistance != 0)
            {
                float repulsionMagnitude = (repulsion.force + repulsor.repulsion.force) * (1 / squareDistance);
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