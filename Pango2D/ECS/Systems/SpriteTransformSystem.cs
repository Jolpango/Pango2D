using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.Contracts;
namespace Pango2D.ECS.Systems
{
    public class SpriteTransformSystem : UpdateComponentSystem<SpriteComponent, TransformComponent>
    {
        protected override void Update(GameTime gameTime, Entity entity, SpriteComponent c1, TransformComponent c2)
        {
            c1.Sprite.Position = c2.Position;
            c1.Sprite.Rotation = c2.Rotation;
            c1.Sprite.Scale = c2.Scale;
        }
    }
}
