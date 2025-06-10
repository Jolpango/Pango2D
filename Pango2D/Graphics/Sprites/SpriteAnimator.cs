using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pango2D.Graphics.Sprites
{
    public class SpriteAnimator
    {
        private readonly Dictionary<string, SpriteAnimation> animations;
        private SpriteAnimation current;
        private float time;

        public Rectangle CurrentFrame => current?.Frames[currentFrameIndex].SourceRect ?? Rectangle.Empty;
        private int currentFrameIndex;

        public SpriteAnimator(Dictionary<string, SpriteAnimation> animations)
        {
            this.animations = animations;
        }

        public void Play(string name, bool loop = false)
        {
            if (!animations.TryGetValue(name, out var anim)) return;
            current = anim;
            time = 0f;
            currentFrameIndex = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (current == null || current.Frames.Length == 0) return;

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            float currentDuration = current.Frames[currentFrameIndex].Duration > 0
                ? current.Frames[currentFrameIndex].Duration
                : current.DefaultFrameDuration;

            if (time >= currentDuration)
            {
                time = 0f;
                currentFrameIndex++;
                if (currentFrameIndex >= current.Frames.Length)
                {
                    if (current.Looping)
                        currentFrameIndex = 0;
                    else
                        currentFrameIndex = current.Frames.Length - 1;
                }
            }
        }

        public Rectangle GetCurrentFrameRect() =>
            current?.Frames[currentFrameIndex].SourceRect ?? Rectangle.Empty;
    }

}
