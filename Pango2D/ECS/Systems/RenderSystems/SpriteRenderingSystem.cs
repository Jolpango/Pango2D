using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS.Components;

namespace Pango2D.ECS.Systems.RenderSystems
{
    public class SpriteRenderingSystem : DrawComponentSystem<SpriteComponent>
    {

        public SpriteRenderingSystem()
        {
            RenderPhase = RenderPhase.World;
        }
        

        protected override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Entity entity, SpriteComponent component)
        {
            component.Sprite.Draw(spriteBatch);
        }
    }
}
