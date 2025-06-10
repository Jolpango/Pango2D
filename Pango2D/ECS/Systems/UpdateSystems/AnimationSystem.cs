using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;

namespace Pango2D.ECS.Systems.UpdateSystems
{
    public class AnimationSystem : PostUpdateComponentSystem<AnimationComponent, SpriteComponent>
    {
        protected override void PostUpdate(GameTime gameTime, Entity entity, AnimationComponent animation, SpriteComponent sprite)
        {
            animation.Animator.Update(gameTime);
            
            sprite.Sprite.SourceRectangle = animation.Animator.CurrentFrame;
        }
    }
}
