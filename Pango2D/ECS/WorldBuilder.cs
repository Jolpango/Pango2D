

using Pango2D.ECS.Systems.Contracts;

namespace Pango2D.ECS
{
    public class WorldBuilder
    {
        private readonly World world;
        public WorldBuilder()
        {
            world = new World();
        }
        public WorldBuilder AddSystem(ISystem system)
        {
            world.AddSystem(system);
            return this;
        }
        public World Build()
        {
            return world;
        }
    }
}
