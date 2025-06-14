using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pango2D.Core.Input.Contracts;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems;

namespace Demo
{
    public class PlayerInputSystem : PreUpdateComponentSystem<PlayerComponent, Velocity>
    {
        private readonly IInputProvider input;
        public PlayerInputSystem(IInputProvider input)
        {
            this.input = input;
        }

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
            c2.Value = direction * 100f; // Speed can be adjusted as needed
            if (direction != Vector2.Zero)
            {
                World.AddComponent(entity, new AnimationCommand() { AnimationName = "default" });
            }
        }
    }
}
