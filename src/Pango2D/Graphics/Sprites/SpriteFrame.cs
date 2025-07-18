using Microsoft.Xna.Framework;

namespace Pango2D.Graphics.Sprites
{
    public class SpriteFrame
    {
        public Rectangle SourceRect { get; }
        public float Duration { get; } // Optional per-frame duration

        public SpriteFrame(Rectangle sourceRect, float duration = -1f)
        {
            SourceRect = sourceRect;
            Duration = duration;
        }
    }

}
