using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace PlatformerDemo.Components
{
    public class HealthBarComponent : IComponent
    {
        public int Width { get; set; } = 100;
        public int Height { get; set; } = 10;
        public Vector2 Offset { get; set; } = new Vector2(0, 0);
    }
}
