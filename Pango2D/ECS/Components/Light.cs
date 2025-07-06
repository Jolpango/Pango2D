using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components
{
    public enum LightType
    {
        Point,
        Ambient
    }
    public class Light : IComponent
    {
        public LightType Type { get; set; } = LightType.Point;
        public Vector2 Offset = Vector2.Zero;
        public float Radius = 100f;
        public float Intensity = 1f;
        public Color Color = Color.White;
        public Texture2D Texture = null;
    }
}
