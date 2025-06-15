

using Pango2D.Core;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.ECS.Systems.RenderSystems;
using Pango2D.ECS.Systems.UpdateSystems;

namespace Pango2D.ECS
{
    /// <summary>
    /// Provides a fluent interface for constructing and configuring a <see cref="World"/> instance.
    /// </summary>
    /// <remarks>The <see cref="WorldBuilder"/> class allows for the step-by-step creation and configuration
    /// of a  <see cref="World"/> object by adding systems and other components. It supports chaining methods for a
    /// fluent API design, enabling concise and readable world setup code.</remarks>
    public class WorldBuilder
    {
        private readonly World world;
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldBuilder"/> class.
        /// </summary>
        /// <remarks>This constructor creates a new <see cref="WorldBuilder"/> instance and initializes an
        /// internal <see cref="World"/> object. Use this class to configure and build a <see cref="World"/>
        /// instance.</remarks>
        public WorldBuilder(GameServices services)
        {
            world = new World(services);
        }

        /// <summary>
        /// Adds the specified system to the world being built.
        /// </summary>
        /// <param name="system">The system to add. Must implement the <see cref="ISystem"/> interface.</param>
        /// <returns>The current <see cref="WorldBuilder"/> instance, allowing for method chaining.</returns>
        public WorldBuilder AddSystem(ISystem system)
        {
            world.AddSystem(system);
            return this;
        }
        /// <summary>
        /// Adds the core systems required for the world to function.
        /// </summary>
        /// <remarks>This method registers a predefined set of essential systems, including animation,
        /// rendering,  and movement systems, to the world. These systems are necessary for basic world operations and
        /// are always included.</remarks>
        /// <returns>The current <see cref="WorldBuilder"/> instance, allowing for method chaining.</returns>
        public WorldBuilder AddCoreSystems()
        {
            // Add core systems that are always needed in a world
            world.AddSystem(new AnimationSystem());
            world.AddSystem(new AnimationCommandSystem());
            world.AddSystem(new SpriteRenderSystem());
            world.AddSystem(new MovementSystem());
            return this;
        }

        /// <summary>
        /// Builds and returns the constructed <see cref="World"/> instance.
        /// </summary>
        /// <returns>The constructed <see cref="World"/> instance.</returns>
        public World Build()
        {
            return world;
        }
    }
}
