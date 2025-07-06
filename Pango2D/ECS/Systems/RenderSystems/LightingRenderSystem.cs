
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components;
using Pango2D.ECS.Services;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Extensions;
using System.Linq;

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
            var camera = World.Query<Camera>().FirstOrDefault().Item2;
            var viewMatrix = camera?.GetViewMatrix() ?? Matrix.Identity;
            spriteBatch.GraphicsDevice.SetRenderTarget(renderTargetRegistry[RenderTargetId.Lightmap]);
            renderPassSettings.TransformMatrix = viewMatrix;
            spriteBatch.Begin(renderPassSettings);
            foreach (var (_, ambientLight) in World.Query<Light>((_, light) => light.Type == LightType.Ambient))
            {
                spriteBatch.Draw(
                    TextureCache.White,
                    new Rectangle(
                        (int)camera.Position.X - camera.ViewportWidth / 2,
                        (int)camera.Position.Y - camera.ViewportHeight / 2,
                        camera.ViewportWidth,
                        camera.ViewportHeight),
                    ambientLight.Color
                );
            }
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
