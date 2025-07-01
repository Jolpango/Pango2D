using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Systems.Contracts;
using System.Collections.Generic;

namespace Pango2D.ECS.Systems
{
    public abstract class PreUpdateComponentSystem<T1> : IPreUpdateSystem where T1 : IComponent
    {
        public World World { get; set; }

        public virtual void Initialize() { }

        public virtual void PreUpdate(GameTime gameTime)
        {
            foreach (var (entity, component) in World.Query<T1>())
            {
                PreUpdate(gameTime, entity, component);
            }
        }
        protected abstract void PreUpdate(GameTime gameTime, Entity entity, T1 component);
    }

    public abstract class PreUpdateComponentSystem<T1, T2> : IPreUpdateSystem
        where T1 : IComponent
        where T2 : IComponent
    {
        public World World { get; set; }

        public virtual void Initialize() { }

        public virtual void PreUpdate(GameTime gameTime)
        {
            foreach (var (entity, c1, c2) in World.Query<T1, T2>())
            {
                PreUpdate(gameTime, entity, c1, c2);
            }
        }
        protected abstract void PreUpdate(GameTime gameTime, Entity entity, T1 c1, T2 c2);
    }
    public abstract class PreUpdateComponentSystem<T1, T2, T3> : IPreUpdateSystem
    where T1 : IComponent
    where T2 : IComponent
    where T3 : IComponent
    {
        public World World { get; set; }

        public virtual void Initialize() { }

        public virtual void PreUpdate(GameTime gameTime)
        {
            foreach (var (entity, c1, c2, c3) in World.Query<T1, T2, T3>())
            {
                PreUpdate(gameTime, entity, c1, c2, c3);
            }
        }
        protected abstract void PreUpdate(GameTime gameTime, Entity entity, T1 c1, T2 c2, T3 c3);
    }
    public abstract class PreUpdateComponentSystem<T1, T2, T3, T4> : IPreUpdateSystem
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent
    {
        public World World { get; set; }

        public virtual void Initialize() { }

        public virtual void PreUpdate(GameTime gameTime)
        {
            foreach (var (entity, c1, c2, c3, c4) in World.Query<T1, T2, T3, T4>())
            {
                PreUpdate(gameTime, entity, c1, c2, c3, c4);
            }
        }
        protected abstract void PreUpdate(GameTime gameTime, Entity entity, T1 c1, T2 c2, T3 c3, T4 c4);
    }
}
