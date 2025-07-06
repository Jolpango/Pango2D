using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems.Contracts;

namespace Pango2D.ECS.Systems.UpdateSystems.Movement
{
    internal class CollisionResolutionSystem : IPostUpdateSystem
    {
        public World World { get; set; }

        public void Initialize()
        {

        }

        public void PostUpdate(GameTime gameTime)
        {
            foreach (var (entity, collisionEvent) in World.Query<CollisionEvent>())
            {
                if (entity.Id < collisionEvent.Other.Id)
                {
                    ResolveCollision(entity, collisionEvent.Other, collisionEvent.Normal, collisionEvent.PenetrationDepth);
                }
            }
        }

        private void ResolveCollision(Entity entityA, Entity entityB, Vector2 normal, float penetrationDepth)
        {
            if (World.TryGetComponent<Collider>(entityA, out var colliderA) &&
               World.TryGetComponent<Collider>(entityB, out var colliderB))
            {
                if (colliderA.Behavior == ColliderBehavior.Dynamic &&
                    colliderB.Behavior == ColliderBehavior.Dynamic &&
                    World.TryGetComponent<Velocity>(entityA, out var velocityA) &&
                    World.TryGetComponent<Velocity>(entityB, out var velocityB) &&
                    World.TryGetComponent<Mass>(entityA, out var massA) &&
                    World.TryGetComponent<Mass>(entityB, out var massB))
                {
                    ResolveCollision(entityA, colliderA, velocityA, massA, entityB, colliderB, velocityB, massB, normal, penetrationDepth);
                }
                else if (colliderA.Behavior == ColliderBehavior.Dynamic &&
                        colliderB.Behavior == ColliderBehavior.Static)
                {
                    ResolveCollision(entityA, colliderA, entityB, colliderB, normal, penetrationDepth);
                }
                else if (colliderA.Behavior == ColliderBehavior.Static &&
                        colliderB.Behavior == ColliderBehavior.Dynamic)
                {
                    ResolveCollision(entityB, colliderB, entityA, colliderA, -normal, penetrationDepth);
                }
            }
        }

        /// <summary>
        /// Dynamic vs static collision resolution.
        /// </summary>
        /// <param name="dynamicEntity"></param>
        /// <param name="dynamicCollider"></param>
        /// <param name="staticEntity"></param>
        /// <param name="staticCollider"></param>
        /// <param name="normal"></param>
        private void ResolveCollision(Entity dynamicEntity, Collider dynamicCollider,
                                      Entity staticEntity, Collider staticCollider, Vector2 normal, float penetrationDepth)
        {
            if (World.TryGetComponent<Velocity>(dynamicEntity, out var velocity) &&
                World.TryGetComponent<Acceleration>(dynamicEntity, out var acceleration))
            {
                float vn = Vector2.Dot(velocity.Value, normal);
                velocity.Value -= vn * normal;

                if (World.TryGetComponent<Transform>(dynamicEntity, out var transform))
                {
                    transform.Position += normal * penetrationDepth;
                }
            }
        }

        /// <summary>
        /// Dynamic vs dynamic collision resolution.
        /// </summary>
        /// <param name="entityA"></param>
        /// <param name="colliderA"></param>
        /// <param name="entityB"></param>
        /// <param name="colliderB"></param>
        /// <param name="normal"></param>
        private void ResolveCollision(Entity entityA,
            Collider colliderA,
            Velocity velocityA,
            Mass massA,
            Entity entityB,
            Collider colliderB,
            Velocity velocityB,
            Mass massB,
            Vector2 normal,
            float penetrationDepth)
        {
            Vector2 relativeVelocity = velocityB.Value - velocityA.Value;
            float velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);

            float e = 0.8f;
            float j = -(1 + e) * velocityAlongNormal;
            j /= (1 / massA.Value + 1 / massB.Value);
            Vector2 impulse = j * normal;
            velocityA.Value -= impulse / massA.Value;
            velocityB.Value += impulse / massB.Value;

            if (World.TryGetComponent<Transform>(entityA, out var transformA) &&
                World.TryGetComponent<Transform>(entityB, out var transformB))
            {
                float totalMass = massA.Value + massB.Value;
                float ratioA = massB.Value / totalMass;
                float ratioB = massA.Value / totalMass; 

                Vector2 correction = normal * penetrationDepth;
                transformA.Position += correction * ratioA;
                transformB.Position -= correction * ratioB;
            }
        }
    }
}
