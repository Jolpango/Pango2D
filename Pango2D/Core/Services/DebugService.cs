using Microsoft.Xna.Framework;

namespace Pango2D.Core.Services
{
    public class DebugService
    {
        private float fps = 0f;
        public float FPS { get => fps; }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (dt > 0)
                fps = 1f / dt;
        }
    }
}
