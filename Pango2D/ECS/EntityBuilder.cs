using Pango2D.ECS.Components.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.ECS
{
    public class EntityBuilder
    {
        private readonly Entity entity;
        private readonly World world;
        public EntityBuilder(World world)
        {
            this.world = world ?? throw new ArgumentNullException(nameof(world));
            entity = world.CreateEntity();
        }
        public EntityBuilder AddComponent<T>(T component) where T : IComponent
        {
            world.AddComponent(entity, component);
            return this;
        }
        public Entity Build()
        {
            return entity;
        }
    }
}
