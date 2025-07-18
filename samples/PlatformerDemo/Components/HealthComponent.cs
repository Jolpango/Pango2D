using Pango2D.ECS.Components.Contracts;

namespace PlatformerDemo.Components
{
    public class HealthComponent : IComponent
    {
        public int Health { get; set; } = 100;
        public int MaxHealth { get; set; } = 100;
    }
}
