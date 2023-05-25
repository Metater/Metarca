using Metarca.Server.Ecs;
using Metarca.Server.Physics.Config;
using Metarca.Server.Physics.Types;
using System.Numerics;

namespace Metarca.Server.Physics;

public class Entity : ISteppable, ITickable, IEntityListener
{
    private const float DefaultVelocityEpsilon = 1f / 256f;

    private ColliderConfig collider = new();
    private BoundsConfig bounds = new();
    private DragConfig drag = new();
    private RepulsionConfig repulsion = new();
    private VelocityEpsilonConfig velocityEpsilon = new(DefaultVelocityEpsilon);

    private uint? occupiedCellIndex = null;
    private Vector2? position = null;
    private Vector2? velocity = null;

    private readonly EntityEvents events;

    public Zone Zone { get; private set; }
    public uint OccupiedCellIndex
    {
        get
        {
            return occupiedCellIndex!.Value;
        }
        set
        {
            uint? oldOccupiedCellIndex = occupiedCellIndex;
            occupiedCellIndex = value;

            Zone.AddEntity(this, value);
            if (oldOccupiedCellIndex.HasValue)
            {
                Zone.RemoveEntity(this, oldOccupiedCellIndex.Value);
            }

            events.OnCellChange(oldOccupiedCellIndex, value);
        }
    }
    public Vector2 Position
    {
        get
        {
            return position!.Value;
        }
        private set
        {
            if (position.HasValue && value == position.Value)
            {
                return;
            }

            Vector2? oldPosition = position;
            position = value;

            events.OnMove(oldPosition, value);

            uint actualCellIndex = Zone.GetCellIndex(value);
            if (actualCellIndex != OccupiedCellIndex)
            {
                OccupiedCellIndex = actualCellIndex;
            }
        }
    }
    public Vector2 Velocity
    {
        get
        {
            return velocity!.Value;
        }
        set
        {
            if (velocity.HasValue && value == velocity.Value)
            {
                return;
            }

            Vector2? oldVelocity = velocity;
            velocity = value;

            events.OnAccelerate(oldVelocity, value);
        }
    }
    public ulong LayerMask { get; set; } = ulong.MaxValue;

    public ColliderConfig Collider => collider;
    public RepulsionConfig Repulsion => repulsion;

    public Entity(Zone zone, Vector2 position, Vector2 velocity, params IEntityListener[] listeners)
    {
        List<IEntityListener> entityListeners = new() { this };
        entityListeners.AddRange(listeners);
        events = new(entityListeners);

        Zone = zone;
        OccupiedCellIndex = Zone.GetCellIndex(position);
        Position = position;
        Velocity = velocity;

        zone.SpawnEntity(this);
    }

    #region Builder
    public Entity WithCollider(ColliderConfig collider)
    {
        this.collider = collider;
        return this;
    }
    public Entity WithBounds(BoundsConfig bounds)
    {
        this.bounds = bounds;
        return this;
    }
    public Entity WithDrag(DragConfig drag)
    {
        this.drag = drag;
        return this;
    }
    public Entity WithRepulsion(RepulsionConfig repulsion)
    {
        this.repulsion = repulsion;
        return this;
    }
    public Entity WithVelocityEpsilon(VelocityEpsilonConfig velocityEpsilon)
    {
        this.velocityEpsilon = velocityEpsilon;
        return this;
    }
    #endregion

    public void Step(double time, double deltaTime)
    {
        // TODO Add static colliders like efficax-game, dont merge into entities unnaturally
        // TODO Add drag
        // TODO Add collisions
        // TODO try teleport method, check bounds and nearby colliders
        // TODO check position method in Zone, used for checking if it is safe to spawn an entity

        events.OnEarlyStep(time, deltaTime);

        if (repulsion.canBeRepulsed)
        {
            Vector2 force = Vector2.Zero;
            IEnumerator<Entity> nearby = Zone.GetNearbyRepulsors(OccupiedCellIndex);
            while (nearby.MoveNext())
            {
                Entity repulsor = nearby.Current;
                if (repulsor == this) continue;
                force += GetRepulsionForce(repulsor);
                repulsor.events.OnOtherRepulse(this);
                events.OnSelfRepulse(repulsor);
            }
            ApplyForce(force, deltaTime);
        }

        if (velocityEpsilon.enabled)
        {
            if (Velocity.X != 0 && MathF.Abs(Velocity.X) < velocityEpsilon.epsilon)
            {
                Velocity = new(0, Velocity.Y);
            }

            if (Velocity.Y != 0 && MathF.Abs(Velocity.Y) < velocityEpsilon.epsilon)
            {
                Velocity = new(Velocity.X, 0);
            }
        }

        if (Velocity != Vector2.Zero)
        {
            Vector2 desiredPosition = Position;
            if (!collider.enabled)
            {
                desiredPosition = GetNextPosition(deltaTime);
            }
            else
            {

            }

            if (bounds.enabled)
            {
                StopDirection boundsDirection = StopDirection.None;

                if (desiredPosition.Y > bounds.collider.North)
                {
                    boundsDirection |= StopDirection.North;
                    desiredPosition.Y = bounds.collider.North;
                    Velocity = new(Velocity.X, 0);
                }
                else if (desiredPosition.Y < bounds.collider.South)
                {
                    boundsDirection |= StopDirection.South;
                    desiredPosition.Y = bounds.collider.South;
                    Velocity = new(Velocity.X, 0);
                }

                if (desiredPosition.X > bounds.collider.East)
                {
                    boundsDirection |= StopDirection.East;
                    desiredPosition.X = bounds.collider.East;
                    Velocity = new(0, Velocity.Y);
                }
                else if (desiredPosition.X < bounds.collider.West)
                {
                    boundsDirection |= StopDirection.West;
                    desiredPosition.X = bounds.collider.West;
                    Velocity = new(0, Velocity.Y);
                }

                if (boundsDirection != StopDirection.None)
                {
                    events.OnBoundsStop(boundsDirection);
                }
            }

            Position = desiredPosition;

            if (drag.enabled)
            {
                Velocity *= 1.0f - (drag.force * (float)deltaTime);
            }
        }

        events.OnLateStep(time, deltaTime);
    }

    public void Tick()
    {
        events.OnTick();
    }

    public void ApplyForce(Vector2 force, double deltaTime)
    {
        Velocity += force * (float)deltaTime;
    }

    public void Despawn()
    {
        Zone.DespawnEntity(this);
    }

    private Vector2 GetRepulsionForce(Entity repulsor)
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

    private Vector2 GetNextPosition(double deltaTime)
    {
        return Position + (Velocity * (float)deltaTime);
    }
}