
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core;
using Pango2D.Core.Contracts;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components;
using Pango2D.ECS.Services;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Extensions;

namespace Pango2D.ECS.Systems.RenderSystems
{
    public class LightingRenderSystem : IDrawSystem
    {
        public RenderPhase RenderPhase { get; set; } = RenderPhase.Lighting;
        public World World { get; set; }
        private RenderPassSettings renderPassSettings = new()
        {
            SortMode = SpriteSortMode.Immediate,
            BlendState = BlendState.Additive,
        };
        private LightRendererService lightRenderer;
        private LightBufferService lightBufferService;
        public void Initialize()
        {
            lightRenderer = World.Services.Get<LightRendererService>();
            lightBufferService = World.Services.Get<LightBufferService>();
        }

        public void BeginDraw(SpriteBatch spriteBatch)
        {
            var viewMatrix = World.Services.TryGet<ICameraService>()?.GetViewMatrix() ?? Matrix.Identity;
            spriteBatch.GraphicsDevice.SetRenderTarget(lightRenderer.LightMap);
            spriteBatch.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1, 1);
            renderPassSettings.TransformMatrix = viewMatrix;
            spriteBatch.Begin(renderPassSettings);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(var lightInstance in lightBufferService.ActiveLights)
            {
                spriteBatch.Draw(
                    lightRenderer.RadialTexture,
                    lightInstance.WorldPosition,
                    null,
                    lightInstance.Color * lightInstance.Intensity,
                    0f,
                    new Vector2(lightRenderer.RadialTexture.Width / 2, lightRenderer.RadialTexture.Height / 2),
                    Vector2.One * lightInstance.Radius, // TODO: Handle scale properly
                    SpriteEffects.None,
                    0f);
            }
        }

        public void EndDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.GraphicsDevice.SetRenderTarget(null);
        }

    }
}
