using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Commands;
using Pango2D.Graphics.Sprites;

namespace Pango2D.ECS.Systems.UpdateSystems.Animation
{
    public class AnimationCommandSystem : CommandComponentSystem<AnimationCommand, SpriteAnimator>
    {
        protected override void Execute(GameTime gameTime, Entity entity, AnimationCommand command, SpriteAnimator target)
        {
            if (command.Stop) { target.Stop(); return; }
            if (command.Pause) { target.Pause(); return; }
            if (command.Resume) { target.Resume(); return; }
            if (command.Queue) { target.Queue(command.AnimationName, loop: command.Loop, onEnd: command.OnEnd); return; }

            if (command.SetAsDefault)
                target.DefaultAnimation = command.AnimationName;

            target.Play(command.AnimationName,
                loop: command.Loop,
                forceRestart: command.ForceRestart,
                onEnd: command.OnEnd,
                onFrameChanged: command.OnFrameChanged);
        }
    }
}
