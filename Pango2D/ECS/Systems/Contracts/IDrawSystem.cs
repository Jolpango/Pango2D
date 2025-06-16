using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;

namespace Pango2D.ECS.Systems.Contracts
{
    public interface IDrawSystem : ISystem
    {
        public RenderPhase RenderPhase { get; set; }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
