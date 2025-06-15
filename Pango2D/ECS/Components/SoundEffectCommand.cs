using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components
{
    public class SoundEffectCommand : ICommandComponent
    {
        public string SoundEffectName { get; set; }
        public float Volume { get; set; } = 1.0f;
        public float Pitch { get; set; } = 0.0f;
        public float Pan { get; set; } = 0.0f;
    }
}
