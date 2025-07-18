using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components.CameraComponents;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pango2D.ECS.Services
{
    public class RenderPipeline(World world)
    {
        public World World { get; set; } = world ?? throw new ArgumentNullException(nameof(world), "World cannot be null.");
        private RenderTargetRegistry RenderTargets => World.Services.Get<RenderTargetRegistry>();
        private RenderPassSettings worldSettings = new()
        {
            SortMode = SpriteSortMode.FrontToBack,
            BlendState = BlendState.AlphaBlend,
            SamplerState = SamplerState.PointClamp,
            TransformMatrix = Matrix.Identity,
            RasterizerState = RasterizerState.CullNone
        };

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var worldSystems = World.GetDrawSystems(RenderPhase.World);
            var lightingSystems = World.GetDrawSystems(RenderPhase.Lighting);
            var uiSystems = World.GetDrawSystems(RenderPhase.UI);
            var compositeSystems = World.GetDrawSystems(RenderPhase.PostProcess);

            DrawWorldSystems(gameTime, spriteBatch, worldSystems);
#if DEBUG
            DrawDebugSystems(gameTime, spriteBatch, World.GetDrawSystems(RenderPhase.Debug));
#endif
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
            var viewMatrix = World.Query<Camera>().FirstOrDefault().Item2?.GetViewMatrix() ?? Matrix.Identity;
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
        private void DrawDebugSystems(GameTime gameTime, SpriteBatch spriteBatch, IEnumerable<IDrawSystem> worldSystems)
        {
            var viewMatrix = World.Query<Camera>().FirstOrDefault().Item2?.GetViewMatrix() ?? Matrix.Identity;
            worldSettings.TransformMatrix = viewMatrix;
            spriteBatch.GraphicsDevice.SetRenderTarget(RenderTargets[RenderTargetId.Debug]);
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
