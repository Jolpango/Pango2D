using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pango2D.ECS.Systems.Contracts
{
    public interface IPostUpdateSystem : ISystem
    {
        public void PostUpdate(GameTime gameTime, IEnumerable<Entity> entities);
    }
}
