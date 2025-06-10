using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pango2D.ECS.Systems.Contracts
{
    public interface IPreUpdateSystem : ISystem
    {
        public void PreUpdate(GameTime gameTime, IEnumerable<Entity> entities);
    }
}
