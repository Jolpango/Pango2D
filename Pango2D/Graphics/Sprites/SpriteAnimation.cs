namespace Pango2D.Graphics.Sprites
{
    public class SpriteAnimation
    {
        public SpriteAnimationFrame[] Frames { get; }
        public float DefaultFrameDuration { get; }
        public bool Looping { get; }

        public SpriteAnimation(SpriteAnimationFrame[] frames, float defaultDuration = 0.1f, bool looping = false)
        {
            Frames = frames;
            DefaultFrameDuration = defaultDuration;
            Looping = looping;
        }
    }

}
