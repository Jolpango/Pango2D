using Microsoft.Xna.Framework;
using Pango2D.ECS.Systems.Contracts;

namespace Pango2D.ECS.Systems.UpdateSystems.Movement
{
    /// <summary>
    /// A composite system that manages collision detection and resolution.
    /// </summary>
    public class CollisionSystem : IPostUpdateSystem
    {
        public World World { get; set; }
        private readonly CollisionDetectionSystem collisionDetectionSystem;
        private readonly CollisionResolutionSystem collisionResolutionSystem;

        public CollisionSystem()
        {
            collisionDetectionSystem = new CollisionDetectionSystem();
            collisionResolutionSystem = new CollisionResolutionSystem();
        }

        public void Initialize()
        {
            collisionDetectionSystem.World = World;
            collisionResolutionSystem.World = World;
            collisionDetectionSystem.Initialize();
            collisionResolutionSystem.Initialize();
        }
        public void PostUpdate(GameTime gameTime)
        {
            collisionDetectionSystem.PostUpdate(gameTime);
            collisionResolutionSystem.PostUpdate(gameTime);
        }
    }
}
