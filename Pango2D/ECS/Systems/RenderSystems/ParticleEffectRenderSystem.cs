using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Graphics.Particles;

namespace Pango2D.ECS.Systems.RenderSystems
{
    public class ParticleEffectRenderSystem : IDrawSystem
    {
        public RenderPhase RenderPhase { get; set; } = RenderPhase.World;
        public World World { get; set; }

        public ParticleEffectRenderSystem()
        {
            RenderPhase = RenderPhase.World;
        }

        public void Initialize() { }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var (_, particleEffect) in World.Query<ParticleEffect>())
            {
                if (particleEffect.IsActive)
                {
                    particleEffect.Update(gameTime);
                    particleEffect.Draw(spriteBatch);
                }
            }
        }
    }
}
