using Pango2D.ECS.Components.Contracts;
using System.Collections.Generic;
using Pango2D.ECS;
using System.Linq;

namespace Pango2D.ECS.Components
{
    public class ComponentStore<T> where T : IComponent
    {
        private Dictionary<Entity, T> components = new();
        public void Add(Entity e, T component)
        {
            if (components.ContainsKey(e))
            {
                components[e] = component;
            }
            else
            {
                components.Add(e, component);
            }
        }
        public void Remove(Entity e)
        {
            if (components.ContainsKey(e))
            {
                components.Remove(e);
            }
        }
        public bool Has(Entity e) => components.ContainsKey(e);
        public T Get(Entity e) => components.TryGetValue(e, out var component) ? component : default(T);
        public IEnumerable<(Entity, T)> All() => components.Select(kvp => (kvp.Key, kvp.Value));
    }
}
