using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems.Contracts;
using System.Linq;

namespace Pango2D.ECS.Systems.UpdateSystems
{
    public class CollisionSystem : IUpdateSystem
    {
        public World World { get; set; }

        public void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {

            World.RemoveComponents<CollisionEvent>();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var dynamics = World.Query<Transform, Velocity, Collider>().ToList();
            for (int i = 0; i < dynamics.Count; i++)
            {
                var (entityA, transformA, velocityA, colliderA) = dynamics[i];
                Vector2 posA = transformA.Position;
                Vector2 velA = velocityA.Value;
                Rectangle futureA = GetWorldBounds(posA + velA * dt, colliderA.Bounds);

                // Static resolution
                foreach (var (entityStatic, staticTransform, staticCollider) in World.Query<Transform, Collider>((_, _, c) => c.IsStatic))
                {
                    Rectangle staticBounds = GetWorldBounds(staticTransform.Position, staticCollider.Bounds);
                    if (futureA.Intersects(staticBounds))
                    {
                        World.AddComponent(entityA, new CollisionEvent { Other = entityStatic, Normal = Vector2.Zero}); // Notify collision
                        World.AddComponent(entityStatic, new CollisionEvent { Other = entityA, Normal = Vector2.Zero }); // Notify collision
                        if (GetWorldBounds(posA + new Vector2(velA.X * dt, 0), colliderA.Bounds).Intersects(staticBounds))
                            velA.X = 0;

                        if (GetWorldBounds(posA + new Vector2(0, velA.Y * dt), colliderA.Bounds).Intersects(staticBounds))
                            velA.Y = 0;
                    }
                }

                // Dynamic vs Trigger
                foreach (var (entityTrigger, triggerTransform, triggerCollider) in World.Query<Transform, Collider>((_, _, c) => c.IsTrigger))
                {
                    Rectangle triggerBounds = GetWorldBounds(triggerTransform.Position, triggerCollider.Bounds);
                    if (futureA.Intersects(triggerBounds))
                    {
                        World.AddComponent(entityA, new CollisionEvent { Other = entityTrigger, Normal = Vector2.Zero }); // Notify collision
                        World.AddComponent(entityTrigger, new CollisionEvent { Other = entityA, Normal = Vector2.Zero }); // Notify collision
                    }
                }

                // Dynamic vs Dynamic
                for (int j = i + 1; j < dynamics.Count; j++)
                {
                    var (entityB, transformB, velocityB, colliderB) = dynamics[j];
                    Vector2 posB = transformB.Position;
                    Vector2 velB = velocityB.Value;

                    Rectangle futureB = GetWorldBounds(posB + velB * dt, colliderB.Bounds);

                    if (futureA.Intersects(futureB))
                    {
                        World.AddComponent(entityA, new CollisionEvent { Other = entityB, Normal = Vector2.Zero }); // Notify collision
                        World.AddComponent(entityB, new CollisionEvent { Other = entityA, Normal = Vector2.Zero }); // Notify collision
                        velA = Vector2.Zero;
                        velocityB.Value = Vector2.Zero;
                    }
                }

                velocityA.Value = velA;
            }
            foreach(var (e, c) in World.Query<Collider>((e, c) => c.IsTransient))
            {
                World.DestroyEntity(e);
            }
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
