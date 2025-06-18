
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
        private RenderTargetRegistry renderTargetRegistry;
        private LightBufferService lightBufferService;
        public void Initialize()
        {
            renderTargetRegistry = World.Services.Get<RenderTargetRegistry>();
            lightBufferService = World.Services.Get<LightBufferService>();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var viewMatrix = World.Services.TryGet<ICameraService>()?.GetViewMatrix() ?? Matrix.Identity;
            spriteBatch.GraphicsDevice.SetRenderTarget(renderTargetRegistry[RenderTargetId.Lightmap]);
            renderPassSettings.TransformMatrix = viewMatrix;
            spriteBatch.Begin(renderPassSettings);

            foreach (var lightInstance in lightBufferService.ActiveLights)
            {
                var texture = lightInstance.Texture ?? TextureCache.RadialLight;
                Vector2 scale = new(
                    (lightInstance.Radius * 2f) / texture.Width,
                    (lightInstance.Radius * 2f) / texture.Height
                );
                spriteBatch.Draw(
                    texture,
                    lightInstance.WorldPosition,
                    null,
                    lightInstance.Color * lightInstance.Intensity,
                    0f,
                    new(texture.Width / 2f, texture.Height / 2f),
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
            spriteBatch.End();
        }
    }
}
