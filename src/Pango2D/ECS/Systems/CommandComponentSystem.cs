using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Systems.Contracts;

namespace Pango2D.ECS.Systems
{
    public abstract class CommandComponentSystem<TCommand> : IPostUpdateSystem
        where TCommand : ICommandComponent
    {
        public World World { get; set; }

        public void Initialize() { }

        public virtual void PostUpdate(GameTime gameTime)
        {
            foreach (var (entity, command) in World.Query<TCommand>())
            {
                Execute(gameTime, entity, command);
                World.RemoveComponent<TCommand>(entity);
            }
        }

        protected abstract void Execute(GameTime gameTime, Entity entity, TCommand command);
    }
    public abstract class CommandComponentSystem<TCommand, TTarget> : IPostUpdateSystem
        where TCommand : ICommandComponent
        where TTarget : IComponent
    {
        public World World { get; set; }

        public void Initialize() { }

        public virtual void PostUpdate(GameTime gameTime)
        {
            foreach (var (entity, command, target) in World.Query<TCommand, TTarget>())
            {
                Execute(gameTime, entity, command, target);
                World.RemoveComponent<TCommand>(entity);
            }
        }

        protected abstract void Execute(GameTime gameTime, Entity entity, TCommand command, TTarget target);
    }
}
