using Pango2D.ECS.Components.Contracts;

namespace Demo.Content
{
    public class EnemyComponent : IComponent
    {
        public int HealthBarHeight { get; set; } = 10;
        public int HealthBarWidth { get; set; } = 100;
        public int MaxHealth { get; set; } = 100;
        public int Health { get; set; } = 100;
        public string Name { get; set; } = "Enemy";
    }
}
