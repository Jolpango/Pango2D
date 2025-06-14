using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components;
using Pango2D.Graphics.Sprites;

namespace Pango2D.ECS.Systems.RenderSystems
{
    public class SpriteRenderingSystem : DrawComponentSystem<Sprite, Transform>
    {

        public SpriteRenderingSystem()
        {
            RenderPhase = RenderPhase.World;
        }
        

        protected override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Entity entity, Sprite sprite, Transform transform)
        {
            sprite.Draw(spriteBatch, transform);
        }
    }
}
