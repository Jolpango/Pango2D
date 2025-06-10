using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pango2D.Utilities
{
    public static class TextureCache
    {
        public static Texture2D White { get; private set; }

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            if (White is null)
            {
                // Create a 1x1 white texture
                White = new Texture2D(graphicsDevice, 1, 1);
                Color[] colorData = { Color.White };
                White.SetData(colorData);
            }
        }
    }
}
