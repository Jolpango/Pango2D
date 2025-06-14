using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Extensions;
using System.Collections.Generic;

namespace Pango2D.ECS.Systems
{
    public abstract class DrawComponentSystem<T1> : IDrawSystem where T1 : IComponent
    {
        public World World { get; set; }
        public RenderPhase RenderPhase { get; set; } = RenderPhase.World;
        protected RenderPassSettings renderPassSettings = new RenderPassSettings();

        public virtual void BeginDraw(SpriteBatch spriteBatch, Matrix? matrìx = null)
        {
            renderPassSettings.TransformMatrix = matrìx;
            spriteBatch.Begin(renderPassSettings);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(var (entity, component) in World.Query<T1>())
            {
                Draw(gameTime, spriteBatch, entity, component);
            }
        }

        public virtual void EndDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
        }

        protected abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, Entity entity, T1 component);
    }
    public abstract class DrawComponentSystem<T1, T2> : IDrawSystem
        where T1 : IComponent
        where T2 : IComponent
    {
        public World World { get; set; }
        public RenderPhase RenderPhase { get; set; } = RenderPhase.World;
        protected RenderPassSettings renderPassSettings = new RenderPassSettings();

        public virtual void BeginDraw(SpriteBatch spriteBatch, Matrix? matrìx = null)
        {
            renderPassSettings.TransformMatrix = matrìx;
            spriteBatch.Begin(renderPassSettings);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var (entity, c1, c2) in World.Query<T1, T2>())
            {
                Draw(gameTime, spriteBatch, entity, c1, c2);
            }
        }

        public virtual void EndDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
        }

        protected abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, Entity entity, T1 c1, T2 c2);
    }
}