using Pango2D.ECS.Components.Contracts;
using System.Collections.Generic;
using Pango2D.ECS;
using System.Linq;
using System;

namespace Pango2D.ECS.Components
{
    /// <summary>
    /// Represents a store that manages components of type <typeparamref name="T"/> associated with entities.
    /// </summary>
    /// <remarks>The <see cref="ComponentStore{T}"/> provides functionality to add, remove, retrieve, and
    /// query components associated with entities. It is designed to efficiently manage the mapping between entities and
    /// their corresponding components.</remarks>
    /// <typeparam name="T">The type of components managed by the store. Must implement <see cref="IComponent"/>.</typeparam>
    public class ComponentStore<T> where T : IComponent
    {
        private Dictionary<Entity, T> components = new();

        /// <summary>
        /// Adds a component to the store for the specified entity.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="component"></param>
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

        /// <summary>
        /// Removes the specified entity and its associated components from the collection.
        /// </summary>
        /// <remarks>If the specified entity is not present in the collection, no action is
        /// taken.</remarks>
        /// <param name="e">The entity to remove. Must not be null.</param>
        public void Remove(Entity e)
        {
            if (components.ContainsKey(e))
            {
                components.Remove(e);
            }
        }

        /// <summary>
        /// Removes all components from the collection.
        /// </summary>
        /// <remarks>This method clears the entire collection, leaving it empty.  Use this method when you
        /// need to reset the collection or discard all components.</remarks>
        public void RemoveAll()
        {
            components.Clear();
        }

        /// <summary>
        /// Determines whether the specified entity has an associated component.
        /// </summary>
        /// <param name="e">The entity to check for an associated component.</param>
        /// <returns><see langword="true"/> if the specified entity has an associated component;  otherwise, <see
        /// langword="false"/>. </returns>
        public bool Has(Entity e) => components.ContainsKey(e);

        /// <summary>
        /// Retrieves the component associated with the specified entity.
        /// </summary>
        /// <param name="e">The entity whose associated component is to be retrieved.</param>
        /// <returns>The component of type <typeparamref name="T"/> associated with the specified entity,  or the default value
        /// of <typeparamref name="T"/> if the entity does not have an associated component.</returns>
        public T Get(Entity e) => components.TryGetValue(e, out var component) ? component : default(T);

        /// <summary>
        /// Retrieves all entities and their associated components as a collection of tuples.
        /// </summary>
        /// <remarks>The method returns a collection representing the mapping of entities to their
        /// components. Each tuple in the collection contains an entity and the component associated with it.</remarks>
        /// <returns>An <see cref="IEnumerable{T}"/> containing tuples, where each tuple consists of an <see cref="Entity"/> and
        /// its corresponding component of type <typeparamref name="T"/>.</returns>
        public IEnumerable<(Entity, T)> All() => components.Select(kvp => (kvp.Key, kvp.Value));

    }
}
