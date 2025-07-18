using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.Physics;
using Pango2D.ECS.Systems.Contracts;
using System;

namespace Pango2D.ECS.Systems.UpdateSystems.MovementSystems
{
    public class PhysicsSystem : IUpdateSystem
    {
        public World World { get; set; }

        /// <summary>
        /// Gets or sets the friction coefficient used to simulate resistance in motion calculations.
        /// Value should be between 0 and 1, where 0 means no friction and 1 means maximum friction.
        /// </summary>
        public float Friction { get; set; } = 0.85f;

        public void Initialize() { }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var(entity, transform, acceleration, mass, velocity) in World.Query<Transform, Acceleration, Mass, Velocity>())
            {
                World.TryGetComponent<Friction>(entity, out var friction);
                velocity.Value += acceleration.Value * dt;
                transform.Position += velocity.Value * dt;
                if (mass.Value > 0)
                    velocity.Value *= MathF.Pow(1f - friction?.Value ?? Friction, dt);

                acceleration.Value = Vector2.Zero;
            }
        }
    }
}
