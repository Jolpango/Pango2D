using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;
using Pango2D.Graphics.Sprites;

namespace Pango2D.ECS.Systems.UpdateSystems
{
    public class AnimationCommandSystem : CommandComponentSystem<AnimationCommand, SpriteAnimator>
    {
        protected override void Execute(GameTime gameTime, Entity entity, AnimationCommand command, SpriteAnimator target)
        {
            target.Play(command.AnimationName, command.Loop);
        }
    }
}
