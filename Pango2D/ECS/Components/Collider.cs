using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components
{
    public class Collider : IComponent
    {
        public Rectangle Bounds { get; set; }
        public bool IsStatic { get; set; } = false;
        public bool IsTrigger { get; set; } = false;
        public bool IsTransient { get; set; } = false;
    }
}
