using Pango2D.ECS.Components.Contracts;
using System;
using System.Collections.Generic;

namespace Pango2D.ECS
{
    public class Entity
    {
        private readonly Dictionary<Type, IComponent> components = new Dictionary<Type, IComponent>();
        public T AddComponent<T>(T component) where T : IComponent
        {
            components[typeof(T)] = component;
            return component;
        }

        public T GetComponent<T>() where T : IComponent
        {
            components.TryGetValue(typeof(T), out var component);
            return (T)component;
        }

        public bool HasComponent<T>() where T : IComponent
        {
            return components.ContainsKey(typeof(T));
        }

        public bool RemoveComponent(IComponent component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));
            return components.Remove(component.GetType());
        }
    }
}
