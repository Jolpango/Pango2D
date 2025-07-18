using Microsoft.Xna.Framework;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.ECS.Components.CameraComponents;
using System.Linq;
using Pango2D.ECS.Components;

namespace Pango2D.ECS.Systems.UpdateSystems.CameraSystems
{
    public class MainCameraSystem : IPostUpdateSystem
    {
        public World World { get; set; }

        public void Initialize() { }

        public void PostUpdate(GameTime gameTime)
        {
            var (cameraEntity, camera) = World.Query<Camera>().FirstOrDefault();
            if (camera is null)
                return;

            var targetTuple = World.Query<MainCameraTarget, Transform>().FirstOrDefault();
            if (targetTuple.Item3 is not Transform transform)
                return;

            World.TryGetComponent(cameraEntity, out CameraAcceleration acceleration);
            if (acceleration is null)
            {
                camera.Position = transform.Position + camera.Offset;
                camera.Velocity = Vector2.Zero;
                return;
            }

            float accel = acceleration.Acceleration;
            float maxSpeed = acceleration.MaxSpeed;
            float friction = acceleration.Friction;

            Vector2 targetPosition = transform.Position + camera.Offset;

            Vector2 toTarget = targetPosition - camera.Position;
            camera.Velocity += toTarget * accel;
            if (camera.Velocity.Length() > maxSpeed)
                camera.Velocity = Vector2.Normalize(camera.Velocity) * maxSpeed;

            camera.Position += camera.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            camera.Velocity *= friction;

            if (camera.Velocity.LengthSquared() < 100f)
                camera.Velocity = Vector2.Zero;
        }
    }
}
