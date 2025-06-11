using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Extensions;
using System;
using System.Collections.Generic;

namespace Pango2D.ECS
{
    /// <summary>
    /// Represents a game world that manages entities and systems for updating and rendering.
    /// </summary>
    /// <remarks>The <see cref="World"/> class provides functionality to create and manage entities, as well
    /// as  add systems for updating and drawing those entities. Systems are categorized into update systems  and draw
    /// systems, which are executed during the respective <see cref="Update(GameTime)"/> and  <see cref="Draw"/>
    /// methods.</remarks>
    public class World
    {
        private readonly Dictionary<Type, ComponentStore<IComponent>> componentStores = new ();
        private readonly HashSet<Entity> entities = new ();
        private int entityIdCounter = 0;
        private readonly Dictionary<RenderPhase, List<IDrawSystem>> drawSystems = new ();
        private readonly List<IPreUpdateSystem> preUpdateSystems = new ();
        private readonly List<IUpdateSystem> updateSystems = new ();
        private readonly List<IPostUpdateSystem> postUpdateSystems = new ();

        /// <summary>
        /// Creates a new instance of the <see cref="Entity"/> class and adds it to the internal collection.
        /// </summary>
        /// <remarks>The created entity is automatically added to the internal collection of entities. 
        /// Callers can use the returned instance to configure or manipulate the entity as needed.</remarks>
        /// <returns>A new instance of the <see cref="Entity"/> class.</returns>
        public Entity CreateEntity()
        {
            var entity = new Entity(entityIdCounter++);
            entities.Add(entity);
            return entity;
        }

        /// <summary>
        /// Adds a component of the specified type to the given entity.
        /// </summary>
        /// <remarks>If a component store for the specified type does not already exist, a new store is
        /// created. This method ensures that the component is properly associated with the entity.</remarks>
        /// <typeparam name="T">The type of the component to add. Must implement <see cref="IComponent"/>.</typeparam>
        /// <param name="entity">The entity to which the component will be added.</param>
        /// <param name="component">The component instance to add to the entity.</param>
        public void AddComponent<T>(Entity entity, T component) where T : IComponent
        {
            if (!componentStores.TryGetValue(typeof(T), out var store))
            {
                store = new ComponentStore<IComponent>();
                componentStores[typeof(T)] = store;
            }
            store.Add(entity, component);
        }
        /// <summary>
        /// Removes a component of the specified type from the given entity.
        /// </summary>
        /// <remarks>If the specified component type is not present in the entity, no action is
        /// taken.</remarks>
        /// <typeparam name="T">The type of the component to remove. Must implement <see cref="IComponent"/>.</typeparam>
        /// <param name="entity">The entity from which the component will be removed.</param>
        public void RemoveComponent<T>(Entity entity) where T : IComponent
        {
            if (componentStores.TryGetValue(typeof(T), out var store))
            {
                store.Remove(entity);
            }
        }

        public IEnumerable<(Entity, T1)> Query<T1>() where T1 : IComponent
        {
            GetStore<T1>(out var store);
            foreach (var (entity, component) in store.All())
            {
                yield return (entity, (T1)component);
            }
        }

        public IEnumerable<(Entity, T1, T2)> Query<T1, T2>()
            where T1 : IComponent
            where T2 : IComponent
        {
            GetStore<T1>(out var store1);
            GetStore<T2>(out var store2);
            foreach (var (entity, c1) in store1.All())
            {
                if(store2.Has(entity))
                {
                    yield return (entity, (T1)c1, (T2)store2.Get(entity));
                }
            }
        }


        /// <summary>
        /// Adds a system to the appropriate collection based on its type.
        /// </summary>
        /// <remarks>Systems implementing <see cref="IDrawSystem"/> are added to the draw system
        /// collection,  while systems implementing <see cref="IUpdateSystem"/> are added to the update system
        /// collection.</remarks>
        /// <param name="system">The system to add. Must implement either <see cref="IDrawSystem"/> or <see cref="IUpdateSystem"/>.</param>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="system"/> does not implement <see cref="IDrawSystem"/> or <see
        /// cref="IUpdateSystem"/>.</exception>
        public void AddSystem(ISystem system)
        {
            system.World = this;
            if (system is IDrawSystem drawSystem)
            {
                if(!drawSystems.ContainsKey(drawSystem.RenderPhase))
                    drawSystems[drawSystem.RenderPhase] = new List<IDrawSystem>();
                drawSystems[drawSystem.RenderPhase].Add(drawSystem);
            }
            else if (system is IUpdateSystem updateSystem)
            {
                updateSystems.Add(updateSystem);
            }
            else if (system is IPreUpdateSystem preUpdateSystem)
            {
                preUpdateSystems.Add(preUpdateSystem);
            }
            else if (system is IPostUpdateSystem postUpdateSystem)
            {
                postUpdateSystems.Add(postUpdateSystem);
            }
            else
            {
                throw new ArgumentException("Unsupported system type", nameof(system));
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var system in preUpdateSystems)
                system.PreUpdate(gameTime);
            foreach (var system in updateSystems)
                system.Update(gameTime);
            foreach (var system in postUpdateSystems)
                system.PostUpdate(gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawPhase(RenderPhase.World, gameTime, spriteBatch);
            DrawPhase(RenderPhase.UI, gameTime, spriteBatch);
            DrawPhase(RenderPhase.PostProcess, gameTime, spriteBatch);
            DrawPhase(RenderPhase.Debug, gameTime, spriteBatch);
        }

        private void DrawPhase(RenderPhase phase, GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var system in GetDrawSystems(phase))
            {
                system.BeginDraw(spriteBatch);
                system.Draw(gameTime, spriteBatch);
                system.EndDraw(spriteBatch);
            }
        }

        private IEnumerable<IDrawSystem> GetDrawSystems(RenderPhase phase)
        {
            if (drawSystems.TryGetValue(phase, out var systems))
            {
                return systems;
            }
            return Array.Empty<IDrawSystem>();
        }

        private void GetStore<T>(out ComponentStore<IComponent> store) where T : IComponent
        {
            if (!componentStores.TryGetValue(typeof(T), out store))
            {
                store = new ComponentStore<IComponent>();
                componentStores[typeof(T)] = store;
            }
        }
    }
}
