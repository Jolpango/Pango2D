using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pango2D.Core.Input.Contracts;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems;
using Pango2D.Graphics.Sprites;
using System;

namespace Demo
{
    public class PlayerInputSystem(IInputProvider input) : PreUpdateComponentSystem<PlayerComponent, Velocity>
    {
        private readonly IInputProvider input = input;

        protected override void PreUpdate(GameTime gameTime, Entity entity, PlayerComponent c1, Velocity c2)
        {
            Vector2 direction = Vector2.Zero;
            if (input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.Up))
                direction.Y -= 1;
            if (input.IsKeyDown(Keys.S) || input.IsKeyDown(Keys.Down))
                direction.Y += 1;
            if (input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.Left))
                direction.X -= 1;
            if (input.IsKeyDown(Keys.D) || input.IsKeyDown(Keys.Right))
                direction.X += 1;

            direction = direction.LengthSquared() > 0 ? Vector2.Normalize(direction) : Vector2.Zero;
            Vector2 previousDirection = c2.Value.LengthSquared() > 0 ? Vector2.Normalize(c2.Value) : Vector2.Zero;
            c2.Value = direction * 100f; // Speed can be adjusted as needed

            // Determine animation name based on direction, prioritizing left/right
            string? animationName = null;
            if (direction.X < 0)
                animationName = "left";
            else if (direction.X > 0)
                animationName = "right";
            else if (direction.Y < 0)
                animationName = "up";
            else if (direction.Y > 0)
                animationName = "down";

            if(direction == Vector2.Zero)
            {
                World.AddComponent(entity, new AnimationCommand()
                {
                    Pause = true
                });
            }
            else if (animationName != null)
            {
                World.AddComponent(entity, new AnimationCommand()
                {
                    AnimationName = animationName,
                    Loop = true,
                });
            }

            if (input.IsKeyPressed(Keys.Space))
            {
                World.AddComponent(entity, new SoundEffectCommand() { SoundEffectName = "swing", Pitch = Random.Shared.NextSingle() });
            }
        }
    }
}
