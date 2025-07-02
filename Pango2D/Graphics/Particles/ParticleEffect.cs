using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pango2D.Graphics.Particles
{
    public class ParticleEffect
    {
        public List<ParticleEmitter> Emitters { get; set; } = [];
        public bool IsActive { get; set; } = true;
        public Vector2 Position { get; set; } = Vector2.Zero;
        public void Update(GameTime gameTime)
        {
            if (!IsActive) return;
            foreach (var emitter in Emitters)
            {
                emitter.Update(gameTime);
            }
        }
        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (!IsActive) return;
            foreach (var emitter in Emitters)
            {
                emitter.Draw(spriteBatch, Position);
            }
        }
    }
}
