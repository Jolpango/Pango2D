using Pango2D.ECS.Components.Contracts;

namespace Demo
{
    public class PlayerComponent : IComponent
    {
        public int Health { get; set; } = 100;
        public int Gold { get; set; } = 0;
    }
}
