using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Contracts;
using Pango2D.ECS.Components;

namespace Pango2D.Core
{
    public class CameraService : ICameraService
    {
        private readonly Camera camera;
        private Viewport viewport;

        public CameraService(Camera camera, Viewport viewport)
        {
            this.camera = camera;
            this.viewport = viewport;
        }

        public Vector2 GetPosition()
        {
            return camera.Position;
        }

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-camera.Position, 0f)) *
               Matrix.CreateRotationZ(camera.Rotation) *
               Matrix.CreateScale(camera.Zoom, camera.Zoom, 1f) *
               Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0f));
        }

        public void Move(Vector2 vector2)
        {
            camera.Position += vector2;
        }

        public void SetPosition(Vector2 position)
        {
            camera.Position = position;
        }
    }
}
