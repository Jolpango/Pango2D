using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pango2D.Core.Services
{
    public class DebugService
    {
        private readonly Queue<float> frameTimes = new();
        private float timeWindow = 2f; // seconds to average over
        private float totalTime = 0f;
        private float fps = 0f;
        public float FPS => fps;

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameTimes.Enqueue(dt);
            totalTime += dt;

            // Remove old frames outside the time window
            while (totalTime > timeWindow && frameTimes.Count > 0)
            {
                totalTime -= frameTimes.Dequeue();
            }

            if (totalTime > 0f)
                fps = frameTimes.Count / totalTime;
        }
    }
}
