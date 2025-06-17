using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pango2D.Core.Graphics
{
    public enum RenderTargetId
    {
        World,
        Lightmap, // For lighting effects
        Composition, // For composition effects
        Custom, // For user-defined render targets
        UI,
        Debug
    }
    public class RenderTargetRegistry
    {
        private readonly Dictionary<RenderTargetId, RenderTarget2D> renderTargets = new();
        private readonly GraphicsDevice graphics;
        public RenderTargetRegistry(GraphicsDevice graphics)
        {
            this.graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
            // Initialize default render targets
            renderTargets[RenderTargetId.World] = CreateRenderTarget();
            renderTargets[RenderTargetId.Lightmap] = CreateRenderTarget();
            renderTargets[RenderTargetId.Composition] = CreateRenderTarget();
            renderTargets[RenderTargetId.UI] = CreateRenderTarget();
            renderTargets[RenderTargetId.Debug] = CreateRenderTarget();
        }

        public RenderTarget2D Create(RenderTargetId id)
        {
            var renderTarget = CreateRenderTarget();
            renderTargets.Add(id, renderTarget);
            return renderTarget;
        }

        public void ClearRenderTargets()
        {
            graphics.SetRenderTarget(renderTargets[RenderTargetId.World]);
            graphics.Clear(Color.White);
            graphics.SetRenderTarget(renderTargets[RenderTargetId.Lightmap]);
            graphics.Clear(new Color(50, 50, 50, 255));
            graphics.SetRenderTarget(renderTargets[RenderTargetId.Composition]);
            graphics.Clear(Color.Transparent);
            graphics.SetRenderTarget(renderTargets[RenderTargetId.UI]);
            graphics.Clear(Color.Transparent);
#if DEBUG
            graphics.SetRenderTarget(renderTargets[RenderTargetId.Debug]);
            graphics.Clear(Color.Transparent);
#endif
        }

        public RenderTarget2D this [RenderTargetId id]
        {
            get => Get(id);
            set
            {
                if (renderTargets.ContainsKey(id))
                {
                    renderTargets[id] = value;
                }
                else
                {
                    throw new KeyNotFoundException($"Render target with id '{id}' does not exist.");
                }
            }
        }

        public RenderTarget2D Get(RenderTargetId id)
        {
            if (renderTargets.TryGetValue(id, out var renderTarget))
            {
                return renderTarget;
            }
            throw new KeyNotFoundException($"Render target with id '{id}' was not found.");
        }

        private RenderTarget2D CreateRenderTarget()
        {
            var renderTarget = new RenderTarget2D(
                graphics,
                graphics.Viewport.Width,
                graphics.Viewport.Height,
                false, // mipMap: usually false unless needed
                SurfaceFormat.Color, // or whatever format you need
                DepthFormat.Depth24Stencil8, // or DepthFormat.Depth24Stencil8 if using depth
                0, // multiSampleCount
                RenderTargetUsage.PreserveContents // important: keeps contents between uses
            ); return renderTarget;
        }

        public List<RenderTarget2D> GetAll()
        {
            return renderTargets.Values.ToList();
        }
    }
}
