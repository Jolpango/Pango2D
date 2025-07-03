using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pango2D.Graphics.Particles
{
    public class Particle
    {
        private Texture2D texture;
        public Vector2 Origin { get; private set; }
        public Texture2D Texture
        {
            get => texture;
            set
            {
                texture = value;
                Origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            }
        }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public float Rotation { get; set; }
        public float AngularVelocity { get; set; }
        public float Scale { get; set; }
        public Color Color { get; set; }
        public float Opacity { get; set; } = 1f;
        public float Lifetime { get; set; }
        public float MaxLifetime { get; set; }
        public bool IsActive { get; set; }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            if (IsActive)
            {
                spriteBatch.Draw(
                    Texture,
                    Position + Origin + offset,
                    null,
                    Color * Opacity,
                    Rotation,
                    Origin,
                    Scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
