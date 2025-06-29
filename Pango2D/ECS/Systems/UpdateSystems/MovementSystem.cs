
using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;

namespace Pango2D.ECS.Systems.UpdateSystems
{
    public class MovementSystem : PostUpdateComponentSystem<Transform, Velocity>
    {
        protected override void PostUpdate(GameTime gameTime, Entity entity, Transform transform, Velocity velocity)
        {
            transform.Position += velocity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
