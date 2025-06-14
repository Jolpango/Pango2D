using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components
{
    public class Velocity : IComponent
    {
        public Vector2 Value;
        public Velocity(Vector2 value)
        {
            Value = value;
        }
    }
}
