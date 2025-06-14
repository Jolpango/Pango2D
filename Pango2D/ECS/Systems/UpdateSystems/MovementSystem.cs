
using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;

namespace Pango2D.ECS.Systems.UpdateSystems
{
    public class MovementSystem : UpdateComponentSystem<Transform, Velocity>
    {
        protected override void Update(GameTime gameTime, Entity entity, Transform transform, Velocity velocity)
        {
            transform.Position += velocity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
