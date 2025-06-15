using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using System;

namespace Pango2D.ECS.Services
{
    public class LightRendererService
    {
        public RenderTarget2D LightMap { get; private set; }
        public Texture2D RadialTexture { get; }
        public Color AmbientColor { get; set; } = new Color(0.15f, 0.15f, 0.15f);
        private readonly GraphicsDevice graphics;

        public LightRendererService(GraphicsDevice graphics)
        {
            this.graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            RadialTexture = TextureCache.RadialLight;
            Resize(graphics.Viewport.Width, graphics.Viewport.Height);
        }

        public void Resize(int width, int height)
        {
            LightMap?.Dispose();
            LightMap = new RenderTarget2D(graphics, width, height);
        }
    }
}
