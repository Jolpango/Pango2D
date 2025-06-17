using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Contracts;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Extensions;
using System;
using System.Collections.Generic;

namespace Pango2D.ECS.Services
{
    public class RenderPipeline
    {
        public World World { get; set; }
        private RenderTargetRegistry RenderTargets => World.Services.Get<RenderTargetRegistry>();
        private RenderPassSettings worldSettings = new RenderPassSettings
        {
            SortMode = SpriteSortMode.FrontToBack,
            BlendState = BlendState.AlphaBlend,
            SamplerState = SamplerState.PointClamp,
            TransformMatrix = Matrix.Identity
        };
        public RenderPipeline(World world)
        {
            World = world ?? throw new ArgumentNullException(nameof(world), "World cannot be null.");
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var worldSystems = World.GetDrawSystems(Core.Graphics.RenderPhase.World);
            var lightingSystems = World.GetDrawSystems(Core.Graphics.RenderPhase.Lighting);
            var uiSystems = World.GetDrawSystems(Core.Graphics.RenderPhase.UI);
            var compositeSystems = World.GetDrawSystems(Core.Graphics.RenderPhase.PostProcess);

            DrawWorldSystems(gameTime, spriteBatch, worldSystems);
            DrawLightingSystems(gameTime, spriteBatch, lightingSystems);
            DrawUI(gameTime, spriteBatch, uiSystems);
            DrawComposition(gameTime, spriteBatch, compositeSystems);
        }

        private static void DrawComposition(GameTime gameTime, SpriteBatch spriteBatch, IEnumerable<IDrawSystem> compositeSystems)
        {
            foreach (var system in compositeSystems)
            {
                if (system is IDrawSystem drawSystem)
                {
                    drawSystem.Draw(gameTime, spriteBatch);
                }
            }
        }

        private static void DrawUI(GameTime gameTime, SpriteBatch spriteBatch, IEnumerable<IDrawSystem> uiSystems)
        {
            foreach (var system in uiSystems)
            {
                if (system is IDrawSystem drawSystem)
                {
                    drawSystem.Draw(gameTime, spriteBatch);
                }
            }
        }

        private static void DrawLightingSystems(GameTime gameTime, SpriteBatch spriteBatch, IEnumerable<IDrawSystem> lightingSystems)
        {
            foreach (var system in lightingSystems)
            {
                if (system is IDrawSystem drawSystem)
                {
                    drawSystem.Draw(gameTime, spriteBatch);
                }
            }
        }

        private void DrawWorldSystems(GameTime gameTime, SpriteBatch spriteBatch, IEnumerable<IDrawSystem> worldSystems)
        {
            var viewMatrix = World.Services.TryGet<ICameraService>()?.GetViewMatrix() ?? Matrix.Identity;
            worldSettings.TransformMatrix = viewMatrix;
            spriteBatch.GraphicsDevice.SetRenderTarget(RenderTargets[RenderTargetId.World]);
            spriteBatch.Begin(worldSettings);
            foreach (var system in worldSystems)
            {
                if (system is IDrawSystem drawSystem)
                {
                    drawSystem.Draw(gameTime, spriteBatch);
                }
            }
            spriteBatch.End();
        }
    }
}
