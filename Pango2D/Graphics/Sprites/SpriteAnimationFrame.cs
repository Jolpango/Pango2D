using Microsoft.Xna.Framework;

namespace Pango2D.Graphics.Sprites
{
    public class SpriteAnimationFrame
    {
        public Rectangle SourceRect { get; }
        public float Duration { get; } // Optional per-frame duration

        public SpriteAnimationFrame(Rectangle sourceRect, float duration = -1f)
        {
            SourceRect = sourceRect;
            Duration = duration;
        }
    }

}
