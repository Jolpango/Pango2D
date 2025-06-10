using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Systems.Contracts;
using System.Collections.Generic;

namespace Pango2D.ECS.Systems
{
    public abstract class DrawComponentSystem<T1> : IDrawSystem where T1 : IComponent
    {
        public RenderPhase RenderPhase { get; set; } = RenderPhase.World;
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IEnumerable<Entity> entities)
        {
            foreach(var entity in entities)
            {
                if (entity.HasComponent<T1>())
                {
                    Draw(gameTime, spriteBatch, entity, entity.GetComponent<T1>());
                }
            }
        }
        protected abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, Entity entity, T1 component);
    }
    public abstract class DrawComponentSystem<T1, T2> : IDrawSystem
        where T1 : IComponent
        where T2 : IComponent
    {
        public RenderPhase RenderPhase { get; set; } = RenderPhase.World;
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.HasComponent<T1>())
                {
                    Draw(gameTime,
                        spriteBatch,
                        entity,
                        entity.GetComponent<T1>(),
                        entity.GetComponent<T2>());
                }
            }
        }
        protected abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, Entity entity, T1 c1, T2 c2);
    }
    public abstract class DrawComponentSystem<T1, T2, T3> : IDrawSystem
    where T1 : IComponent
    where T2 : IComponent
    where T3 : IComponent
    {
        public RenderPhase RenderPhase { get; set; } = RenderPhase.World;
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.HasComponent<T1>())
                {
                    Draw(
                        gameTime,
                        spriteBatch,
                        entity,
                        entity.GetComponent<T1>(),
                        entity.GetComponent<T2>(),
                        entity.GetComponent<T3>());
                }
            }
        }
        protected abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, Entity entity, T1 c1, T2 c2, T3 c3);
    }
}
