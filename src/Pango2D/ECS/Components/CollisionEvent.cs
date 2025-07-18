﻿using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;
using System.Collections.Generic;

namespace Pango2D.ECS.Components
{
    public class CollisionEvent : IComponent
    {
        public Entity Other { get; init; }
        public Vector2 Normal { get; init; }
        public float PenetrationDepth { get; set; } = 0f;
    }
    public class CollisionEvents : IComponent
    {
        public List<CollisionEvent> Events { get; init; } = [];
    }
}
