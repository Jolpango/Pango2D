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
        public EntityBuilder(Entity entity)
        {
            this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }
        public EntityBuilder AddComponent<T>(T component) where T : IComponent
        {
            entity.AddComponent(component);
            return this;
        }
        public Entity Build()
        {
            return entity;
        }
    }
}
