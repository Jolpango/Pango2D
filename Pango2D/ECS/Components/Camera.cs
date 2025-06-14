using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components
{
    public class Camera : IComponent
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public float Zoom { get; set; } = 1f;
        public float Rotation { get; set; } = 0f;
    }
}
