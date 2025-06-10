using Pango2D.ECS.Components.Contracts;
using Pango2D.Graphics.Sprites;

namespace Pango2D.ECS.Components
{
    public class AnimationComponent : IComponent
    {
        public SpriteAnimator Animator { get; set; }
        public AnimationComponent(SpriteAnimator animator)
        {
            Animator = animator;
        }
    }
}
