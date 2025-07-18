using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Physics;
using Pango2D.ECS.Systems.Contracts;

namespace Pango2D.ECS.Systems.UpdateSystems.MovementSystems
{
    public class GravitySystem : IPreUpdateSystem
    {
        public World World { get; set; }

        public void Initialize() { }

        public void PreUpdate(GameTime gameTime)
        {
            foreach (var (_, acceleration, gravity) in World.Query<Acceleration, Gravity>())
            {
                acceleration.Value += gravity.Value * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
