using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.Graphics.Sprites
{
    public class Sprite(Texture2D texture) : IComponent
    {
        public Texture2D Texture { get; set; } = texture;
        public Rectangle SourceRectangle { get; set; } = new Rectangle(0, 0, texture.Width, texture.Height);
        public Vector2 Origin { get => new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2); }
        public Color Color { get; set; } = Color.White;
        public float Alpha { get; set; } = 1f;
        public float LayerDepth { get; set; } = 0f;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, SourceRectangle, Color * Alpha, 0, Origin, Vector2.One, Effects, LayerDepth);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 scale, float rotation)
        {
            spriteBatch.Draw(Texture, position, SourceRectangle, Color * Alpha, rotation, Origin, scale, Effects, LayerDepth);
        }
        public void Draw(SpriteBatch spriteBatch, Transform transform)
        {
            spriteBatch.Draw(Texture, new Vector2((int)transform.Position.X + (int)Origin.X, (int)transform.Position.Y + (int)Origin.Y), SourceRectangle, Color * Alpha, transform.Rotation, Origin, transform.Scale, Effects, LayerDepth);
        }
    }
}
