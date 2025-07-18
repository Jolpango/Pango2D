using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components.Physics
{
    public class Friction : IComponent
    {
        public float Value { get; set; } = 0.85f;
        public Friction() { }
        public Friction(float value)
        {
            Value = value;
        }
    }
}
