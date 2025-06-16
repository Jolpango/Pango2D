using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Contracts;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Extensions;
using Pango2D.Graphics.Sprites;

namespace Pango2D.ECS.Systems.RenderSystems
{
    /// <summary>
    /// A system responsible for rendering sprites in the game world.
    /// </summary>
    /// <remarks>This system is designed to render entities that have both a <see cref="Sprite"/> and a <see
    /// cref="Transform"/> component. It operates during the world rendering phase and applies the current camera's view
    /// matrix to the rendering process.</remarks>
    public class SpriteRenderSystem : IDrawSystem
    {
        private const string RenderTargetName = "World";
        private RenderPassSettings renderPassSettings = new();
        public RenderPhase RenderPhase { get; set; } = RenderPhase.World;
        public World World { get; set; }

        private RenderTargetRegistry renderTargetRegistry;
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteRenderSystem"/> class.
        /// </summary>
        /// <remarks>This constructor sets the default render phase to <see
        /// cref="RenderPhase.World"/>.</remarks>
        public SpriteRenderSystem()
        {
            RenderPhase = RenderPhase.World;
            renderPassSettings.BlendState = BlendState.AlphaBlend;
            renderPassSettings.SamplerState = SamplerState.PointClamp;
        }

        public void Initialize()
        {
            renderTargetRegistry = World.Services.Get<RenderTargetRegistry>();
            renderTargetRegistry.GetOrCreate(RenderTargetName);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var viewMatrix = World.Services.TryGet<ICameraService>()?.GetViewMatrix() ?? Matrix.Identity;
            spriteBatch.GraphicsDevice.SetRenderTarget(renderTargetRegistry.Get(RenderTargetName));
            renderPassSettings.TransformMatrix = viewMatrix;
            spriteBatch.Begin(renderPassSettings);
            foreach (var (entity, transform, sprite) in World.Query<Transform, Sprite>())
            {
                sprite.Draw(spriteBatch, transform);
            }
            spriteBatch.End();
        }
    }
}
