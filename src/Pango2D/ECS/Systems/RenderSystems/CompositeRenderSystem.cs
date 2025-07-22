using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.Core.Services;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Extensions;

namespace Pango2D.ECS.Systems.RenderSystems
{
    public class CompositeRenderSystem : IDrawSystem
    {
        public RenderPhase RenderPhase { get; set; } = RenderPhase.PostProcess;
        public World World { get; set; }
        private RenderPassSettings multiplyPass = new()
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
            RasterizerState = RasterizerState.CullNone,
            Effect = null,
            TransformMatrix = Matrix.Identity
        };

        private RenderPassSettings spritePass = new()
        {
            SortMode = SpriteSortMode.Immediate,
            BlendState = BlendState.AlphaBlend,
            SamplerState = SamplerState.PointClamp,
            DepthStencilState = DepthStencilState.None,
            RasterizerState = RasterizerState.CullNone,
            Effect = null,
            TransformMatrix = Matrix.Identity
        };

        private RenderPassSettings uiPass = new()
        {
            SortMode = SpriteSortMode.Immediate,
            BlendState = BlendState.AlphaBlend,
            SamplerState = SamplerState.PointClamp,
            DepthStencilState = DepthStencilState.None,
            RasterizerState = RasterizerState.CullNone,
            Effect = null,
            TransformMatrix = Matrix.Identity
        };

        private RenderPassSettings backBufferPass = new()
        {
            SortMode = SpriteSortMode.Immediate,
            BlendState = BlendState.AlphaBlend,
            SamplerState = SamplerState.PointClamp
        };
        private RenderTargetRegistry renderTargetRegistry;
        private ViewportService viewportService;
        public void Initialize()
        {
            renderTargetRegistry = World.Services.Get<RenderTargetRegistry>();
            viewportService = World.Services.Get<ViewportService>();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.SetRenderTarget(renderTargetRegistry[RenderTargetId.Composition]);
            spriteBatch.Begin(spritePass);
            spriteBatch.Draw(renderTargetRegistry[RenderTargetId.World], Vector2.Zero, Color.White);
            spriteBatch.End();

            spriteBatch.Begin(multiplyPass);
            spriteBatch.Draw(renderTargetRegistry[RenderTargetId.Lightmap], Vector2.Zero, Color.White);
            spriteBatch.End();

            spriteBatch.Begin(uiPass);
            spriteBatch.Draw(renderTargetRegistry[RenderTargetId.UI], Vector2.Zero, Color.White);
#if DEBUG
            spriteBatch.Draw(renderTargetRegistry[RenderTargetId.Debug], Vector2.Zero, Color.White);
#endif
            spriteBatch.End();

            spriteBatch.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(backBufferPass);
            spriteBatch.Draw(renderTargetRegistry[RenderTargetId.Composition], viewportService.DestinationRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
