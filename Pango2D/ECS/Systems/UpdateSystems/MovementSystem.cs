
using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;

namespace Pango2D.ECS.Systems.UpdateSystems
{
    public class MovementSystem : UpdateComponentSystem<TransformComponent, VelocityComponent>
    {
        protected override void Update(GameTime gameTime, Entity entity, TransformComponent transform, VelocityComponent velocity)
        {
            transform.Position += velocity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
