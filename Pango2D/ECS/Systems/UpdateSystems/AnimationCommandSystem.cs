using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;

namespace Pango2D.ECS.Systems.UpdateSystems
{
    public class AnimationCommandSystem : UpdateComponentSystem<AnimationCommandComponent, AnimationComponent>
    {
        protected override void Update(GameTime gameTime, Entity entity, AnimationCommandComponent command, AnimationComponent animation)
        {
            animation.Animator.Play(command.AnimationName, command.Loop);
            World.RemoveComponent<AnimationCommandComponent>(entity);
        }
    }
}
