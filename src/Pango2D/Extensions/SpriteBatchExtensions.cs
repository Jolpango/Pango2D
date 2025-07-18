
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;

namespace Pango2D.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void Begin(this SpriteBatch spriteBatch, RenderPassSettings settings)
        {
            spriteBatch.Begin(
                settings.SortMode,
                settings.BlendState,
                settings.SamplerState,
                settings.DepthStencilState,
                settings.RasterizerState,
                settings.Effect,
                settings.TransformMatrix
            );
        }
    }
}
