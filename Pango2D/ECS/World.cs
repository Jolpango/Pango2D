using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core;
using Pango2D.Core.Contracts;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Services;
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
    /// <remarks>
    /// Initializes a new instance of the <see cref="World"/> class with the specified game services.
    /// </remarks>
    /// <param name="services">The game services instance used to manage and provide access to game-related functionality. Cannot be <see
    /// langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is <see langword="null"/>.</exception>
    public class World(GameServices services) : IDisposable
    {
        private readonly Dictionary<Type, ComponentStore<IComponent>> componentStores = [];
        private readonly HashSet<Entity> entities = [];
        private int entityIdCounter = 0;
        private readonly Dictionary<RenderPhase, List<IDrawSystem>> drawSystems = [];
        private readonly List<IPreUpdateSystem> preUpdateSystems = [];
        private readonly List<IUpdateSystem> updateSystems = [];
        private readonly List<IPostUpdateSystem> postUpdateSystems = [];

        public GameServices Services { get; init; } = services ?? throw new ArgumentNullException(nameof(services), "GameServices cannot be null.");

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

        /// <summary>
        /// Queries the store for all entities that have a component of the specified type.
        /// </summary>
        /// <remarks>This method retrieves all entities and their associated components of the specified
        /// type from the underlying store. The returned collection is lazily evaluated.</remarks>
        /// <typeparam name="T1">The type of the component to query for. Must implement <see cref="IComponent"/>.</typeparam>
        /// <returns>An enumerable collection of tuples, where each tuple contains an <see cref="Entity"/> and the corresponding
        /// component of type <typeparamref name="T1"/>.</returns>
        public IEnumerable<(Entity, T1)> Query<T1>() where T1 : IComponent
        {
            GetStore<T1>(out var store);
            foreach (var (entity, component) in store.All())
            {
                yield return (entity, (T1)component);
            }
        }

        /// <summary>
        /// Queries and retrieves all entities that have both specified component types.
        /// </summary>
        /// <remarks>This method iterates through all entities that have a component of type <typeparamref
        /// name="T1"/> and checks if they also have a component of type <typeparamref name="T2"/>. If both components
        /// are present, the entity and its components are included in the result.</remarks>
        /// <typeparam name="T1">The type of the first component to query. Must implement <see cref="IComponent"/>.</typeparam>
        /// <typeparam name="T2">The type of the second component to query. Must implement <see cref="IComponent"/>.</typeparam>
        /// <returns>An enumerable collection of tuples, where each tuple contains an entity and its associated components of
        /// type <typeparamref name="T1"/> and <typeparamref name="T2"/>.</returns>
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
                if(!Services.Has<RenderPipeline>())
                    Services.Register(new RenderPipeline(this));

                if (!drawSystems.ContainsKey(drawSystem.RenderPhase))
                    drawSystems[drawSystem.RenderPhase] = [];

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
            system.Initialize();
        }

        /// <summary>
        /// Disposes of the <see cref="World"/> instance, releasing any resources it holds.
        /// </summary>
        public void Dispose()
        {
            //Do any necessary cleanup here, such as disposing of resources or clearing collections
        }

        /// <summary>
        /// Updates the state of the game by executing pre-update, update, and post-update systems in sequence.
        /// </summary>
        /// <remarks>This method processes all registered systems in three distinct phases: pre-update,
        /// update, and post-update. Each phase is executed sequentially, ensuring that systems in each phase are
        /// processed in the order they were added.</remarks>
        /// <param name="gameTime">The current game time, providing timing information for the update cycle.</param>
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
            Services.Get<RenderPipeline>().Draw(gameTime, spriteBatch);
        }

        public IEnumerable<IDrawSystem> GetDrawSystems(RenderPhase phase)
        {
            if (drawSystems.TryGetValue(phase, out var systems))
            {
                return systems;
            }
            return [];
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
