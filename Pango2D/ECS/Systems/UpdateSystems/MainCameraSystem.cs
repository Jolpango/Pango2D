using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems.Contracts;
using System.Linq;

namespace Pango2D.ECS.Systems.UpdateSystems
{
    public class MainCameraSystem : IPostUpdateSystem
    {
        public World World { get; set; }

        public void Initialize() { }

        public void PostUpdate(GameTime gameTime)
        {
            var cameraTuple = World.Query<Camera>().FirstOrDefault();
            if (cameraTuple.Item2 is not Camera camera)
                return;
            var targetTuple = World.Query<MainCameraTarget, Transform>().FirstOrDefault();
            if (targetTuple.Item3 is not Transform transform)
                return;

            Vector2 targetPosition = transform.Position + camera.Offset;

            Vector2 toTarget = targetPosition - camera.Position;
            camera.Velocity += toTarget * 5f;
            if (camera.Velocity.Length() > camera.MaxSpeed)
                camera.Velocity = Vector2.Normalize(camera.Velocity) * camera.MaxSpeed;

            camera.Position += camera.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            camera.Velocity *= camera.Friction;

            if (camera.Velocity.LengthSquared() < 100f)
                camera.Velocity = Vector2.Zero;
        }
    }
}
