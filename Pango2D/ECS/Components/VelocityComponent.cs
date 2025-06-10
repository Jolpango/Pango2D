using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components
{
    public class VelocityComponent : IComponent
    {
        public Vector2 Value;
        public VelocityComponent(Vector2 value)
        {
            Value = value;
        }
    }
}
