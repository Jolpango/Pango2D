using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components
{
    public class BoxCollider : IComponent
    {
        public Rectangle Bounds { get; set; } // Local bounds of the collider(positioned relative to the entity's transform)
        public bool IsStatic { get; set; }
    }
}
