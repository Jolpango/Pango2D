using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components.Physics
{
    public class Acceleration(Vector2 value) : IComponent
    {
        public Vector2 Value = value;
    }
}
