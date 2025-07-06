using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems.Contracts;
using System;
using System.Linq;

namespace Pango2D.ECS.Systems.UpdateSystems.Movement
{
    internal class CollisionDetectionSystem : IPostUpdateSystem
    {
        public World World { get; set; }

        public void Initialize() { }

        public void PostUpdate(GameTime gameTime)
        {
            World.RemoveComponents<CollisionEvent>();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var colliders = World.Query<Transform, Collider>().ToList();

            for (int i = 0; i < colliders.Count; i++)
            {
                var (entityA, transformA, colliderA) = colliders[i];

                for (int j = i + 1; j < colliders.Count; j++)
                {
                    var (entityB, transformB, colliderB) = colliders[j];

                    if (ShouldCheckCollision(colliderA, colliderB))
                    {
                        if (CheckCollision(transformA, colliderA, transformB, colliderB, out Vector2 normal, out float penetrationDepth))
                        {
                            NotifyCollision(entityA, entityB, normal, penetrationDepth);
                        }
                    }
                }
            }

            DestroyTransientColliderEntities();
        }

        private static bool ShouldCheckCollision(Collider a, Collider b)
        {
            if (a.Behavior == ColliderBehavior.Static && b.Behavior == ColliderBehavior.Static)
                return false;
            if (a.Behavior == ColliderBehavior.Trigger && b.Behavior == ColliderBehavior.Trigger)
                return false;
            return true;
        }

        private bool CheckCollision(Transform tA, Collider cA, Transform tB, Collider cB, out Vector2 normal, out float penetrationDepth)
        {
            normal = Vector2.Zero;
            penetrationDepth = 0f;
            if (cA.Type == ColliderType.AABB && cB.Type == ColliderType.AABB)
            {
                var rectA = GetWorldBounds(tA.Position, cA.Bounds);
                var rectB = GetWorldBounds(tB.Position, cB.Bounds);
                if (rectA.Intersects(rectB))
                {
                    float overlapX = Math.Min(rectA.Right, rectB.Right) - Math.Max(rectA.Left, rectB.Left);
                    float overlapY = Math.Min(rectA.Bottom, rectB.Bottom) - Math.Max(rectA.Top, rectB.Top);

                    if (overlapX < overlapY)
                    {
                        normal = (rectA.Center.X < rectB.Center.X) ? new Vector2(-1, 0) : new Vector2(1, 0);
                        penetrationDepth = overlapX;
                    }
                    else
                    {
                        normal = (rectA.Center.Y < rectB.Center.Y) ? new Vector2(0, -1) : new Vector2(0, 1);
                        penetrationDepth = overlapY;
                    }
                    return true;
                }
            }
            else if (cA.Type == ColliderType.Circle && cB.Type == ColliderType.Circle)
            {
                var centerA = tA.Position + cA.CenterOffset;
                var centerB = tB.Position + cB.CenterOffset;
                float distSq = Vector2.DistanceSquared(centerA, centerB);
                float radiusSum = cA.Radius + cB.Radius;
                if (distSq <= radiusSum * radiusSum)
                {
                    normal = Vector2.Normalize(centerB - centerA);
                    penetrationDepth = radiusSum - MathF.Sqrt(distSq);
                    if (penetrationDepth < 0)
                        penetrationDepth = 0;
                    return true;
                }
            }

            else if ((cA.Type == ColliderType.AABB && cB.Type == ColliderType.Circle) ||
                     (cA.Type == ColliderType.Circle && cB.Type == ColliderType.AABB))
            {
                if (cA.Type == ColliderType.Circle)
                {
                    (tA, cA, tB, cB) = (tB, cB, tA, cA);
                }
                var rect = GetWorldBounds(tA.Position, cA.Bounds);
                var circleCenter = tB.Position + cB.CenterOffset;
                float radius = cB.Radius;

                float closestX = Math.Clamp(circleCenter.X, rect.Left, rect.Right);
                float closestY = Math.Clamp(circleCenter.Y, rect.Top, rect.Bottom);
                float distSq = (circleCenter.X - closestX) * (circleCenter.X - closestX) +
                               (circleCenter.Y - closestY) * (circleCenter.Y - closestY);

                if (distSq <= radius * radius)
                {
                    normal = Vector2.Normalize(circleCenter - new Vector2(closestX, closestY));
                    penetrationDepth = radius - MathF.Sqrt(distSq);
                    return true;
                }
            }

            return false;
        }

        private void DestroyTransientColliderEntities()
        {
            foreach (var (e, c) in World.Query<Collider>((e, c) => c.Behavior == ColliderBehavior.Transient))
            {
                World.DestroyEntity(e);
            }
        }

        private void NotifyCollision(Entity entityA, Entity entityB, Vector2 normal, float penetrationDepth)
        {
            World.AddComponent(entityA, new CollisionEvent { Other = entityB, Normal = normal, PenetrationDepth = penetrationDepth });
            World.AddComponent(entityB, new CollisionEvent { Other = entityA, Normal = -normal, PenetrationDepth = penetrationDepth });
        }

        private Rectangle GetWorldBounds(Vector2 position, Rectangle localBounds)
        {
            return new Rectangle(
                (int)(position.X + localBounds.X),
                (int)(position.Y + localBounds.Y),
                localBounds.Width,
                localBounds.Height
            );
        }
    }
}
