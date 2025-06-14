﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pango2D.ECS.Systems.Contracts
{
    public interface IUpdateSystem : ISystem
    {
        public void Update(GameTime gameTime);
    }
}
