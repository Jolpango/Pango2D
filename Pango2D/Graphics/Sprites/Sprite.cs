using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pango2D.Graphics.Sprites
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; } = Vector2.One;
        public float Rotation { get; set; } = 0f;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        public Color Color { get; set; } = Color.White;
        public float Alpha { get; set; } = 1f;
        public float LayerDepth { get; set; } = 0f;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRectangle, Color * Alpha, Rotation, Origin, Scale, Effects, LayerDepth);
        }

    }
}
