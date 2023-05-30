using System.Numerics;

namespace Metarca.Physics.Simulation;

internal class SimulationSystem
{
    private readonly IEntityListener listener;
    private readonly ISurroundingProvider surroundingProvider;
    
    internal SimulationSystem(IEntityListener listener, ISurroundingProvider surroundingProvider)
    {
        this.listener = listener;
        this.surroundingProvider = surroundingProvider;
    }

    internal bool Simulate(Entity entity, double time, double deltaTime)
    {
        listener.OnEarlySimulate(entity, time, deltaTime);

        if (entity.repulsion.canBeRepulsed)
        {
            ApplyRepulsion(entity, deltaTime);
        }

        if (entity.velocityEpsilon.enabled)
        {
            ApplyVelocityEpsilon(entity);
        }

        // In TryTeleport method, check bounds and is collider enabled
        if (entity.Velocity != Vector2.Zero)
        {
            Vector2 nextPosition = entity.Position;
            if (!entity.collider.enabled)
            {
                nextPosition = SimHelper.GetNextPosition(entity, deltaTime);
            }
            else
            {
                if (entity.collider.isTrigger)
                {
                    ApplyTriggers(entity);
                }
                else
                {
                    ApplyCollisions(entity);
                }
            }

            if (entity.bounds.enabled)
            {
                ApplyBounds(entity, ref nextPosition);
            }

            entity.Position = nextPosition;

            ApplyDrag(entity, deltaTime);
        }

        listener.OnLateSimulate(entity, time, deltaTime);
    }

    private void ApplyRepulsion(Entity entity, double deltaTime)
    {
        Vector2 force = Vector2.Zero;
        foreach (var repulsor in surroundingProvider.GetSurroundingEntities(entity.CellId!.Value))
        {
            if (repulsor == entity) continue;
            if (!InteractionHelper.ShouldInteract(entity, repulsor)) continue;
            if (!repulsor.repulsion.canRepulseOthers) continue;
            force += SimHelper.GetForceOnRepulsee(entity, repulsor, out bool interacting);
            if (interacting)
            {
                listener.OnRepulsion(entity, repulsor);
            }
        }
        entity.Velocity += force * (float)deltaTime;
    }
    private static void ApplyVelocityEpsilon(Entity entity)
    {
        if (entity.Velocity.X != 0 && MathF.Abs(entity.Velocity.X) < entity.velocityEpsilon.epsilon)
        {
            entity.Velocity = new(0, entity.Velocity.Y);
        }

        if (entity.Velocity.Y != 0 && MathF.Abs(entity.Velocity.Y) < entity.velocityEpsilon.epsilon)
        {
            entity.Velocity = new(entity.Velocity.X, 0);
        }
    }
    private void ApplyTriggers(Entity entity)
    {
        foreach (var triggerer in surroundingProvider.GetSurroundingEntities(entity.CellId!.Value))
        {
            if (triggerer == entity) continue;
            if (!InteractionHelper.ShouldInteract(entity, triggerer)) continue;

        }
    }
    private void ApplyCollisions(Entity entity)
    {

    }
    private void ApplyBounds(Entity entity, ref Vector2 nextPosition)
    {
        StopDirection direction = StopDirection.None;

        if (nextPosition.Y > entity.bounds.collider.North)
        {
            direction |= StopDirection.North;
            nextPosition.Y = entity.bounds.collider.North;
            entity.Velocity = new(entity.Velocity.X, 0);
        }
        else if (nextPosition.Y < entity.bounds.collider.South)
        {
            direction |= StopDirection.South;
            nextPosition.Y = entity.bounds.collider.South;
            entity.Velocity = new(entity.Velocity.X, 0);
        }

        if (nextPosition.X > entity.bounds.collider.East)
        {
            direction |= StopDirection.East;
            nextPosition.X = entity.bounds.collider.East;
            entity.Velocity = new(0, entity.Velocity.Y);
        }
        else if (nextPosition.X < entity.bounds.collider.West)
        {
            direction |= StopDirection.West;
            nextPosition.X = entity.bounds.collider.West;
            entity.Velocity = new(0, entity.Velocity.Y);
        }

        if (direction != StopDirection.None)
        {
            listener.OnBoundsStop(entity, direction);
        }
    }
    private static void ApplyDrag(Entity entity, double deltaTime)
    {
        entity.Velocity *= 1.0f - (entity.drag.force * (float)deltaTime);
    }
}