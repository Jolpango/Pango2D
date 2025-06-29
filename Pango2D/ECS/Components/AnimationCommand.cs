using Pango2D.ECS.Components.Contracts;
using System;

namespace Pango2D.ECS.Components
{
    public class AnimationCommand : ICommandComponent
    {
        public string AnimationName { get; set; }
        public bool Stop { get; set; } = false;
        public bool Pause { get; set; } = false;
        public bool Resume { get; set; } = false;
        public bool Loop { get; set; } = false;
        public bool ForceRestart { get; set; } = false;
        public bool Queue { get; set; } = false;
        public bool SetAsDefault { get; set; } = false;
        public Action OnEnd { get; set; } = null;
    }
}
