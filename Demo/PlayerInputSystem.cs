using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems;
using Pango2D.Input.Contracts;

namespace Demo
{
    public class PlayerInputSystem : PreUpdateComponentSystem<PlayerComponent, VelocityComponent>
    {
        private readonly IInputProvider input;
        public PlayerInputSystem(IInputProvider input)
        {
            this.input = input;
        }

        protected override void PreUpdate(GameTime gameTime, Entity entity, PlayerComponent c1, VelocityComponent c2)
        {
            Vector2 direction = Vector2.Zero;
            if (input.IsKeyDown(Keys.Right))
            {
                direction.X += 100f;
                entity.AddComponent(new AnimationCommandComponent() { AnimationName = "default", Loop = true });
            }
            if (input.IsKeyDown(Keys.Left))
            {
                direction.X -= 100f;
                entity.AddComponent(new AnimationCommandComponent() { AnimationName = "default", Loop = true });
            }
            if (input.IsKeyDown(Keys.Up))
            {
                direction.Y -= 100f;
                entity.AddComponent(new AnimationCommandComponent() { AnimationName = "default", Loop = true });
            }
            if (input.IsKeyDown(Keys.Down))
            {
                direction.Y += 100f;
                entity.AddComponent(new AnimationCommandComponent() { AnimationName = "default", Loop = true });
            }
            if (direction != Vector2.Zero)
            {
                c2.Value = Vector2.Normalize(direction) * 100f;
            }
        }
    }
}
