
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Contracts;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components;
using Pango2D.Extensions;

namespace Pango2D.ECS.Systems.RenderSystems
{
    public class LightingSystem : DrawComponentSystem<Light, Transform>
    {

        private RenderTarget2D lightMap;
        private Texture2D lightTexture;
        private GraphicsDevice graphics;

        private RenderPassSettings lightPassSettings = new RenderPassSettings
        {
            BlendState = BlendState.Additive,
        };

        private RenderPassSettings multiplyPassSettings = new RenderPassSettings
        {
            BlendState = new BlendState()
            {
                AlphaBlendFunction = BlendFunction.Add,
                AlphaDestinationBlend = Blend.Zero,
                AlphaSourceBlend = Blend.DestinationAlpha,

                ColorBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.DestinationColor,
                ColorDestinationBlend = Blend.Zero
            }
            //BlendState = BlendState.AlphaBlend
            //TODO swisha ludwig 10,000kr
        };

        public LightingSystem() : base()
        {
        }
        public override void Initialize()
        {
            graphics = World.Services.Get<GraphicsDevice>();
            RenderPhase = RenderPhase.PostProcess;
            lightTexture = TextureCache.RadialLight;
            ResizeLightmap(graphics.Viewport.Width, graphics.Viewport.Height); // Default size, can be resized later            
        }

        public override void BeginDraw(SpriteBatch spriteBatch)
        {
            graphics.SetRenderTarget(lightMap);
            graphics.Clear(Color.Green);
            var viewMatrix = World.Services.TryGet<ICameraService>()?.GetViewMatrix() ?? Matrix.Identity;
            lightPassSettings.TransformMatrix = viewMatrix;
            spriteBatch.Begin(lightPassSettings);
        }
        protected override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Entity entity, Light light, Transform transform)
        {
            var pos = transform.Position + light.Offset;
            float scale = light.Radius / (lightTexture.Width / 2f) * 10;

            spriteBatch.Draw(
                lightTexture,
                pos,
                null,
                light.Color * light.Intensity,
                0f,
                new Vector2(lightTexture.Width / 2f),
                scale,
                SpriteEffects.None,
                0f
            );
        }
        public override void EndDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            graphics.SetRenderTarget(null);
            spriteBatch.Begin(multiplyPassSettings);
            spriteBatch.Draw(lightMap, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        private void ResizeLightmap(int width, int height)
        {
            lightMap?.Dispose();
            lightMap = new RenderTarget2D(graphics, width, height);
        }
    }
}
