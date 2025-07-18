using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components.Physics
{
    public class Gravity : IComponent
    {
        public Vector2 Value { get; set; } = new Vector2(0, 98000f);
        public Gravity() { }
        public Gravity(Vector2 value)
        {
            Value = value;
        }

    }
}
