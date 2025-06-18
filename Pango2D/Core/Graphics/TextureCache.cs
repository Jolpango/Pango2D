using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pango2D.Core.Graphics
{
    public static class TextureCache
    {
        public static Texture2D White { get; private set; }
        public static Texture2D RadialLight { get; private set; }

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            if (White is null)
            {
                // Create a 1x1 white texture
                White = new Texture2D(graphicsDevice, 1, 1);
                Color[] colorData = [Color.White];
                White.SetData(colorData);
            }
            if (RadialLight is null)
            {
                // Create a radial light texture
                CreateRadialLightTexture(graphicsDevice);
            }
        }

        public static void CreateRadialLightTexture(GraphicsDevice graphics, int size = 128)
        {
            var tex = new Texture2D(graphics, size, size);
            Color[] data = new Color[size * size];

            Vector2 center = new(size / 2f);
            float radius = size / 2f;

            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                {
                    float dist = Vector2.Distance(center, new Vector2(x, y)) / radius;
                    float alpha = MathHelper.Clamp(1f - dist, 0f, 1f);
                    data[y * size + x] = new Color(Color.White, alpha * alpha); // smooth falloff
                }

            tex.SetData(data);
            RadialLight = tex;
        }
    }
}
