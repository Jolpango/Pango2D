using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components
{
    public class CollisionEvent() : IComponent
    {
        public Entity Other { get; init; }
        public Vector2 Normal { get; init; }
        public float PenetrationDepth { get; set; } = 0f;
    }
}
