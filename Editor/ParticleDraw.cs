using Microsoft.Xna.Framework;
using Pango2D.Core.Graphics;
using Pango2D.Graphics.Particles;

namespace Editor
{
    public class ParticleDraw : MonoGame.Forms.NET.Controls.MonoGameControl
    {
        public ParticleEffect? ParticleEffect { get; set; }
        public Microsoft.Xna.Framework.Color BackgroundColor { get; set; } = Microsoft.Xna.Framework.Color.CornflowerBlue;
        protected override void Initialize()
        {
            TextureCache.Initialize(Editor.GraphicsDevice);
        }
        protected override void Update(GameTime gameTime)
        {
            ParticleEffect?.Update(gameTime);
        }
        protected override void Draw()
        {
            Editor.GraphicsDevice.Clear(BackgroundColor);
            Editor.spriteBatch.Begin();
            ParticleEffect?.Draw(Editor.spriteBatch);
            Editor.spriteBatch.End();
        }
    }
}
