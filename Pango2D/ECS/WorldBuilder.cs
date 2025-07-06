using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.Core.Services;
using Pango2D.ECS.Services;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.ECS.Systems.RenderSystems;
using Pango2D.ECS.Systems.UpdateSystems;
using Pango2D.ECS.Systems.UpdateSystems.Animation;
using Pango2D.ECS.Systems.UpdateSystems.LightSystems;
using Pango2D.ECS.Systems.UpdateSystems.Movement;

namespace Pango2D.ECS
{
    /// <summary>
    /// Provides a fluent interface for constructing and configuring a <see cref="World"/> instance.
    /// </summary>
    /// <remarks>The <see cref="WorldBuilder"/> class allows for the step-by-step creation and configuration
    /// of a  <see cref="World"/> object by adding systems and other components. It supports chaining methods for a
    /// fluent API design, enabling concise and readable world setup code.</remarks>
    /// <remarks>
    /// Initializes a new instance of the <see cref="WorldBuilder"/> class.
    /// </remarks>
    /// <remarks>This constructor creates a new <see cref="WorldBuilder"/> instance and initializes an
    /// internal <see cref="World"/> object. Use this class to configure and build a <see cref="World"/>
    /// instance.</remarks>
    public class WorldBuilder(GameServices services)
    {
        private readonly World world = new(services);

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
            world.AddSystem(new AnimationSystem());
            world.AddSystem(new AnimationCommandSystem());
            world.AddSystem(new SpriteRenderSystem());
            world.AddSystem(new ParticleEffectRenderSystem());
            world.AddSystem(new PhysicsSystem());
            world.AddSystem(new CollisionSystem());
            return this;
        }

        /// <summary>
        /// Adds lighting-related systems to the world, enabling light collection, composition, and rendering.
        /// </summary>
        /// <returns>The current <see cref="WorldBuilder"/> instance, allowing for method chaining.</returns>
        public WorldBuilder AddLightingSystems()
        {
            if (!world.Services.Has<LightBufferService>())
                world.Services.Register(new LightBufferService());

            world.AddSystem(new LightCollectionSystem());
            world.AddSystem(new LightingRenderSystem());
            world.AddSystem(new CompositeRenderSystem());

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
