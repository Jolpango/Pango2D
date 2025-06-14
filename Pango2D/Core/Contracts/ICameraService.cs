using Microsoft.Xna.Framework;

namespace Pango2D.Core.Contracts
{
    public interface ICameraService
    {
        public Matrix GetViewMatrix();
        public void SetPosition(Vector2 position);
        public Vector2 GetPosition();
        void Move(Vector2 vector2);
    }
}
