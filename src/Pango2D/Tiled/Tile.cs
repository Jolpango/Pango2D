using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pango2D.Tiled
{
    public class TileAnimationFrame
    {
        public Rectangle SourceRectangle { get; set; }
        public float Duration { get; set; }
    }
    public class TileAnimation
    {
        public List<TileAnimationFrame> Frames { get; set; }
        public TileAnimationFrame CurrentFrame => Frames.Count > 0 ? Frames[CurrentFrameIndex] : null;
        public int CurrentFrameIndex { get; set; }
        public float TimeSinceLastFrame { get; set; }
        public TileAnimation()
        {
            Frames = new List<TileAnimationFrame>();
            CurrentFrameIndex = 0;
            TimeSinceLastFrame = 0f;
        }
        public void Update(float deltaTime)
        {
            if (Frames.Count == 0) return;
            TimeSinceLastFrame += deltaTime;
            if (TimeSinceLastFrame >= Frames[CurrentFrameIndex].Duration)
            {
                TimeSinceLastFrame -= Frames[CurrentFrameIndex].Duration;
                CurrentFrameIndex = (CurrentFrameIndex + 1) % Frames.Count;
            }
        }
    }
    public class Tile
    {
        public Rectangle DestinationRectangle { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public bool IsAnimated { get; set; } = false;
        public TileAnimation Animation { get; set; }
        public TileSet TileSet { get; set; }

        public Rectangle GetSourceRectangle()
        {
            if (!IsAnimated)
                return SourceRectangle;
            return Animation.CurrentFrame?.SourceRectangle ?? SourceRectangle;
        }

        public void Update(GameTime gameTime)
        {
            if (IsAnimated && Animation != null)
            {
                Animation.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            }
        }
    }
}
