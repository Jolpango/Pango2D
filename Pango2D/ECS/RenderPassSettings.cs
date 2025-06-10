using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pango2D.ECS
{
    public struct RenderPassSettings
    {
        public SpriteSortMode SortMode = SpriteSortMode.Deferred;
        public BlendState BlendState = BlendState.AlphaBlend;
        public SamplerState SamplerState = SamplerState.LinearClamp;
        public DepthStencilState DepthStencilState = DepthStencilState.None;
        public RasterizerState RasterizerState = RasterizerState.CullCounterClockwise;
        public Effect Effect = null;
        public Matrix? TransformMatrix = Matrix.Identity;
        public RenderPassSettings() { }
    }
}
