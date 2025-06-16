using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Services;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Extensions;
using System;

namespace Pango2D.ECS.Systems.RenderSystems
{
    public class LightingCompositeSystem : IDrawSystem
    {
        public RenderPhase RenderPhase { get; set; } = RenderPhase.PostProcess;
        public World World { get; set; }
        private RenderPassSettings renderPassSettings = new()
        {
            SortMode = SpriteSortMode.Immediate,
            BlendState = new BlendState()
            {
                AlphaBlendFunction = BlendFunction.Add,
                AlphaSourceBlend = Blend.DestinationAlpha,
                AlphaDestinationBlend = Blend.Zero,

                ColorBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.DestinationColor,
                ColorDestinationBlend = Blend.Zero
            },
            SamplerState = SamplerState.LinearClamp,
            DepthStencilState = DepthStencilState.None,
            RasterizerState = RasterizerState.CullCounterClockwise,
            Effect = null,
            TransformMatrix = Matrix.Identity
        };
        private LightRendererService lightRenderer;
        private RenderTargetRegistry renderTargetRegistry;
        public void Initialize()
        {
            lightRenderer = World.Services.Get<LightRendererService>();
            renderTargetRegistry = World.Services.Get<RenderTargetRegistry>();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin();
            spriteBatch.Draw(renderTargetRegistry.Get("World"), Vector2.Zero, Color.White);
            spriteBatch.End();

            spriteBatch.Begin(renderPassSettings);
            spriteBatch.Draw(renderTargetRegistry.Get("LightMap"), Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}
