using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components
{
    public class Light : IComponent
    {
        public Vector2 Offset = Vector2.Zero;
        public float Radius = 100f;
        public float Intensity = 1f;
        public Color Color = Color.White;
        public Texture2D Texture = null;
    }
}
