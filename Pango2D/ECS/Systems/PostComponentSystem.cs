using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Systems.Contracts;

namespace Pango2D.ECS.Systems
{
    public abstract class PostUpdateComponentSystem<T1> : IPostUpdateSystem where T1 : IComponent
    {
        public World World { get; set; }
        public virtual void PostUpdate(GameTime gameTime)
        {
            foreach(var (entity, component) in World.Query<T1>())
            {
                PostUpdate(gameTime, entity, component);
            }
        }
        protected abstract void PostUpdate(GameTime gameTime, Entity entity, T1 component);
    }

    public abstract class PostUpdateComponentSystem<T1, T2> : IPostUpdateSystem
        where T1 : IComponent
        where T2 : IComponent
    {
        public World World { get; set; }
        public virtual void PostUpdate(GameTime gameTime)
        {
            foreach (var (entity, c1, c2) in World.Query<T1, T2>())
            {
                PostUpdate(gameTime, entity, c1, c2);
            }
        }
        protected abstract void PostUpdate(GameTime gameTime, Entity entity, T1 c1, T2 c2);
    }
}
