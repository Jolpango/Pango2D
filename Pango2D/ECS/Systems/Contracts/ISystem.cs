using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pango2D.ECS.Systems.Contracts
{
    public interface ISystem
    {
        public World World { get; set; }
        public void Initialize();
    }
}
