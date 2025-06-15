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
        public void Initialize()
        {
            lightRenderer = World.Services.Get<LightRendererService>();
        }

        public void BeginDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(renderPassSettings);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(lightRenderer.LightMap, Vector2.Zero, Color.White);
        }

        public void EndDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
        }
    }
}
