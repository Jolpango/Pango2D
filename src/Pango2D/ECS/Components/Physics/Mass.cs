using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components.Physics
{
    public class Mass(float value) : IComponent
    {
        public float Value = value;
    }
}
