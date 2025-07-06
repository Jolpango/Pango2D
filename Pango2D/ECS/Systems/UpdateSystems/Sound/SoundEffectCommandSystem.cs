using Microsoft.Xna.Framework;
using Pango2D.Core.Audio;
using Pango2D.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.ECS.Systems.UpdateSystems.Sound
{
    public class SoundEffectCommandSystem : CommandComponentSystem<SoundEffectCommand>
    {
        private readonly SoundEffectRegistry soundEffectRegistry;
        public SoundEffectCommandSystem(SoundEffectRegistry soundEffectRegistry)
        {
            this.soundEffectRegistry = soundEffectRegistry ?? throw new ArgumentNullException(nameof(soundEffectRegistry));
        }

        protected override void Execute(GameTime gameTime, Entity entity, SoundEffectCommand command)
        {
            var soundEffect = soundEffectRegistry.Get(command.SoundEffectName);
            if (soundEffect is null)
                throw new InvalidOperationException($"Sound effect '{command.SoundEffectName}' not found in registry.");
            soundEffect.Play(command.Volume, command.Pitch, command.Pan);
        }
    }
}
