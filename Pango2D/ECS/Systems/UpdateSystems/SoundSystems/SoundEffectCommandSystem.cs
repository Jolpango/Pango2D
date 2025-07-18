using Microsoft.Xna.Framework;
using Pango2D.Core.Audio;
using Pango2D.ECS.Components.Commands;
using System;

namespace Pango2D.ECS.Systems.UpdateSystems.SoundSystems
{
    public class SoundEffectCommandSystem(SoundEffectRegistry soundEffectRegistry) : CommandComponentSystem<SoundEffectCommand>
    {
        private readonly SoundEffectRegistry soundEffectRegistry = soundEffectRegistry ?? throw new ArgumentNullException(nameof(soundEffectRegistry));

        protected override void Execute(GameTime gameTime, Entity entity, SoundEffectCommand command)
        {
            var soundEffect = soundEffectRegistry.Get(command.SoundEffectName) ?? throw new InvalidOperationException($"Sound effect '{command.SoundEffectName}' not found in registry.");
            soundEffect.Play(command.Volume, command.Pitch, command.Pan);
        }
    }
}
