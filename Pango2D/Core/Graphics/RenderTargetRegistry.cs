using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Core.Graphics
{
    public class RenderTargetRegistry
    {
        private readonly Dictionary<string, RenderTarget2D> renderTargets = new();
        private readonly GraphicsDevice graphics;
        public RenderTargetRegistry(GraphicsDevice graphics)
        {
            this.graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
        }

        public RenderTarget2D GetOrCreate(string name)
        {
            if (renderTargets.ContainsKey(name))
            {
                return renderTargets[name];
            }
            var renderTarget = CreateRenderTarget();
            renderTargets.Add(name, renderTarget);
            return renderTarget;
        }
        public RenderTarget2D Get(string name)
        {
            if (renderTargets.TryGetValue(name, out var renderTarget))
            {
                return renderTarget;
            }
            throw new KeyNotFoundException($"Render target with name '{name}' not found.");
        }

        private RenderTarget2D CreateRenderTarget()
        {
            var renderTarget = new RenderTarget2D(graphics, graphics.Viewport.Width, graphics.Viewport.Height);
            return renderTarget;
        }
    }
}
