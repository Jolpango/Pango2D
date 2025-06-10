using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Extensions;
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
        private readonly List<Entity> entities = new List<Entity>();
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
            var entity = new Entity();
            entities.Add(entity);
            return entity;
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
                throw new System.ArgumentException("Unsupported system type", nameof(system));
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var system in preUpdateSystems)
                system.PreUpdate(gameTime, entities);
            foreach (var system in updateSystems)
                system.Update(gameTime, entities);
            foreach (var system in postUpdateSystems)
                system.PostUpdate(gameTime, entities);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, RenderPassRegistry registry)
        {
            foreach(var kvp in drawSystems)
            {
                var renderPhase = kvp.Key;
                var systems = kvp.Value;
                switch(renderPhase)
                {
                    case RenderPhase.World:
                        spriteBatch.Begin(registry[RenderPhase.World]);
                        break;
                    case RenderPhase.UI:
                        spriteBatch.Begin(registry[RenderPhase.UI]);
                        break;
                    case RenderPhase.Debug:
                        spriteBatch.Begin(registry[RenderPhase.Debug]);
                        break;
                    default:
                        throw new System.ArgumentOutOfRangeException(nameof(renderPhase), renderPhase, null);
                }
                foreach (var system in systems)
                {
                    system.Draw(gameTime, spriteBatch, entities);
                }
                spriteBatch.End();
            }
        }
    }
}
