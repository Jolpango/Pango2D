﻿using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components
{
    public class AnimationCommand : ICommandComponent
    {
        public string AnimationName { get; set; }
        public bool Loop { get; set; }
    }
}
