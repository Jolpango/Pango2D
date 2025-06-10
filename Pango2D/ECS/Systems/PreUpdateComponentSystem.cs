using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Systems.Contracts;
using System.Collections.Generic;

namespace Pango2D.ECS.Systems
{
    public abstract class PreUpdateComponentSystem<T1> : IPreUpdateSystem where T1 : IComponent
    {
        public void PreUpdate(GameTime gameTime, IEnumerable<Entity> entities)
        {
            foreach(var entity in entities)
            {
                if (entity.HasComponent<T1>())
                {
                    Update(gameTime, entity, entity.GetComponent<T1>());
                }
            }
        }
        protected abstract void Update(GameTime gameTime, Entity entity, T1 component);
    }

    public abstract class PreUpdateComponentSystem<T1, T2> : IPreUpdateSystem
        where T1 : IComponent
        where T2 : IComponent
    {
        public void PreUpdate(GameTime gameTime, IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.HasComponent<T1>() && entity.HasComponent<T2>())
                {
                    PreUpdate(gameTime,
                        entity,
                        entity.GetComponent<T1>(),
                        entity.GetComponent<T2>());
                }
            }
        }
        protected abstract void PreUpdate(GameTime gameTime, Entity entity, T1 c1, T2 c2);
    }
    public abstract class PreUpdateComponentSystem<T1, T2, T3> : IPreUpdateSystem
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
    {
        public void PreUpdate(GameTime gameTime, IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.HasComponent<T1>()
                    && entity.HasComponent<T2>()
                    && entity.HasComponent<T3>())
                {
                    PreUpdate(gameTime,
                        entity,
                        entity.GetComponent<T1>(),
                        entity.GetComponent<T2>(),
                        entity.GetComponent<T3>());
                }
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
        public void PreUpdate(GameTime gameTime, IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.HasComponent<T1>()
                    && entity.HasComponent<T2>()
                    && entity.HasComponent<T3>()
                    && entity.HasComponent<T4>())
                {
                    PreUpdate(gameTime,
                        entity,
                        entity.GetComponent<T1>(),
                        entity.GetComponent<T2>(),
                        entity.GetComponent<T3>(),
                        entity.GetComponent<T4>());
                }
            }
        }
        protected abstract void PreUpdate(GameTime gameTime, Entity entity, T1 c1, T2 c2, T3 c3, T4 c4);
    }
    public abstract class PreUpdateComponentSystem<T1, T2, T3, T4, T5> : IPreUpdateSystem
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent
        where T5 : IComponent
    {
        public void PreUpdate(GameTime gameTime, IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.HasComponent<T1>()
                    && entity.HasComponent<T2>()
                    && entity.HasComponent<T3>()
                    && entity.HasComponent<T4>()
                    && entity.HasComponent<T5>())
                {
                    PreUpdate(gameTime,
                        entity,
                        entity.GetComponent<T1>(),
                        entity.GetComponent<T2>(),
                        entity.GetComponent<T3>(),
                        entity.GetComponent<T4>(),
                        entity.GetComponent<T5>());
                }
            }
        }
        protected abstract void PreUpdate(GameTime gameTime, Entity entity, T1 c1, T2 c2, T3 c3, T4 c4, T5 c5);
    }
    public abstract class PreUpdateComponentSystem<T1, T2, T3, T4, T5, T6> : IPreUpdateSystem
    where T1 : IComponent
    where T2 : IComponent
    where T3 : IComponent
    where T4 : IComponent
    where T5 : IComponent
    where T6 : IComponent
    {
        public void PreUpdate(GameTime gameTime, IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.HasComponent<T1>()
                    && entity.HasComponent<T2>()
                    && entity.HasComponent<T3>()
                    && entity.HasComponent<T4>()
                    && entity.HasComponent<T5>()
                    && entity.HasComponent<T6>())
                {
                    PreUpdate(gameTime,
                        entity,
                        entity.GetComponent<T1>(),
                        entity.GetComponent<T2>(),
                        entity.GetComponent<T3>(),
                        entity.GetComponent<T4>(),
                        entity.GetComponent<T5>(),
                        entity.GetComponent<T6>());
                }
            }
        }
        protected abstract void PreUpdate(GameTime gameTime, Entity entity, T1 c1, T2 c2, T3 c3, T4 c4, T5 c5, T6 c6);
    }
    public abstract class PreUpdateComponentSystem<T1, T2, T3, T4, T5, T6, T7> : IPreUpdateSystem
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent
        where T5 : IComponent
        where T6 : IComponent
        where T7 : IComponent
    {
        public void PreUpdate(GameTime gameTime, IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.HasComponent<T1>()
                    && entity.HasComponent<T2>()
                    && entity.HasComponent<T3>()
                    && entity.HasComponent<T4>()
                    && entity.HasComponent<T5>()
                    && entity.HasComponent<T6>()
                    && entity.HasComponent<T7>())
                {
                    PreUpdate(gameTime,
                        entity,
                        entity.GetComponent<T1>(),
                        entity.GetComponent<T2>(),
                        entity.GetComponent<T3>(),
                        entity.GetComponent<T4>(),
                        entity.GetComponent<T5>(),
                        entity.GetComponent<T6>(),
                        entity.GetComponent<T7>());
                }
            }
        }
        protected abstract void PreUpdate(GameTime gameTime, Entity entity, T1 c1, T2 c2, T3 c3, T4 c4, T5 c5, T6 c6, T7 c7);
    }
}
