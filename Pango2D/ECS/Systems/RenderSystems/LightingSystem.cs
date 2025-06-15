
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Contracts;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components;
using Pango2D.Extensions;

namespace Pango2D.ECS.Systems.RenderSystems
{
    public class LightingSystem : DrawComponentSystem<Light, Transform>
    {
        public LightingSystem() : base()
        {
        }
        public override void Initialize()
        {         
        }

        public override void BeginDraw(SpriteBatch spriteBatch)
        {
        }
        protected override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Entity entity, Light light, Transform transform)
        {
        }
        public override void EndDraw(SpriteBatch spriteBatch)
        {
        }
    }
}
