using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Systems.Contracts;

namespace Pango2D.ECS.Systems
{
    public abstract class UpdateComponentSystem<T1> : IUpdateSystem where T1 : IComponent
    {
        public World World { get; set; }

        public virtual void Initialize() { }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var (entity, component) in World.Query<T1>())
            {
                Update(gameTime, entity, component);
            }
        }
        protected abstract void Update(GameTime gameTime, Entity entity, T1 component);
    }

    public abstract class UpdateComponentSystem<T1, T2> : IUpdateSystem
        where T1 : IComponent
        where T2 : IComponent
    {
        public World World { get; set; }

        public virtual void Initialize() { }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var (entity, c1, c2) in World.Query<T1, T2>())
            {
                Update(gameTime, entity, c1, c2);
            }
        }
        protected abstract void Update(GameTime gameTime, Entity entity, T1 c1, T2 c2);
    }
}
