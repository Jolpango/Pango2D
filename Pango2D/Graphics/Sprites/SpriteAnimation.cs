namespace Pango2D.Graphics.Sprites
{
    public class SpriteAnimation
    {
        public SpriteFrame[] Frames { get; }
        public float DefaultFrameDuration { get; }
        public bool Looping { get; }

        public SpriteAnimation(SpriteFrame[] frames, float defaultDuration = 0.1f, bool looping = false)
        {
            Frames = frames;
            DefaultFrameDuration = defaultDuration;
            Looping = looping;
        }
    }

}
