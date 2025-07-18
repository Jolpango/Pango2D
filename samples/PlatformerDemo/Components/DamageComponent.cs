using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace PlatformerDemo.Components
{
    public class DamageComponent : IComponent
    {
        public float Damage { get; set; }
        public float Knockback { get; set; }
        public Vector2 KnockbackDirection { get; set; } = Vector2.Zero;
        public float StunDuration { get; set; }
    }
}
