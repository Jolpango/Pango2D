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
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var dynamics = World.Query<Transform, Velocity, BoxCollider>().ToList();

            for (int i = 0; i < dynamics.Count; i++)
            {
                var (entityA, transformA, velocityA, colliderA) = dynamics[i];
                Vector2 posA = transformA.Position;
                Vector2 velA = velocityA.Value;
                Rectangle futureA = GetWorldBounds(posA + velA * dt, colliderA.Bounds);

                // Static resolution
                foreach (var (_, staticTransform, staticCollider) in World.Query<Transform, BoxCollider>((_, _, c) => c.IsStatic))
                {
                    Rectangle staticBounds = GetWorldBounds(staticTransform.Position, staticCollider.Bounds);

                    if (futureA.Intersects(staticBounds))
                    {
                        if (GetWorldBounds(posA + new Vector2(velA.X * dt, 0), colliderA.Bounds).Intersects(staticBounds))
                            velA.X = 0;

                        if (GetWorldBounds(posA + new Vector2(0, velA.Y * dt), colliderA.Bounds).Intersects(staticBounds))
                            velA.Y = 0;
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
                        // Simplified: stop both
                        velA = Vector2.Zero;
                        velocityB.Value = Vector2.Zero;
                    }
                }

                velocityA.Value = velA;
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
