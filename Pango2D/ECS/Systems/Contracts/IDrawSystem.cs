using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pango2D.ECS.Systems.Contracts
{
    public interface IDrawSystem : ISystem
    {
        public RenderPhase RenderPhase { get; set; }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IEnumerable<Entity> entities);
    }
}
