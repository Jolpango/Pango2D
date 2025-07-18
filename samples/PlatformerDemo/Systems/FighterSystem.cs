using Microsoft.Xna.Framework;
using Pango2D.ECS;
using Pango2D.ECS.Systems.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerDemo.Systems
{
    public class FighterSystem : IPreUpdateSystem
    {
        public World World { get; set; }
        private readonly FighterIntentSystem fighterIntentSystem = new();
        private readonly FighterMovementSystem fighterMovementSystem = new();
        private readonly FighterActionSystem fighterActionSystem = new();
        private readonly FighterAnimationSystem fighterAnimationSystem = new();

        public void Initialize()
        {
            fighterActionSystem.World = World;
            fighterIntentSystem.World = World;
            fighterMovementSystem.World = World;
            fighterAnimationSystem.World = World;
            fighterIntentSystem.Initialize();
            fighterMovementSystem.Initialize();
            fighterActionSystem.Initialize();
            fighterAnimationSystem.Initialize();
        }

        public void PreUpdate(GameTime gameTime)
        {
            fighterIntentSystem.PreUpdate(gameTime);
            fighterMovementSystem.PreUpdate(gameTime);
            fighterActionSystem.PreUpdate(gameTime);
            fighterAnimationSystem.PreUpdate(gameTime);
        }
    }
}
