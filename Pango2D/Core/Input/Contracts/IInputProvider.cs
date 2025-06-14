using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pango2D.Core.Input;

namespace Pango2D.Core.Input.Contracts
{
    public interface IInputProvider
    {
        public bool IsKeyDown(Keys key);
        public bool IsKeyUp(Keys key);
        public bool IsKeyPressed(Keys key);
        public bool IsKeyReleased(Keys key);
        public bool IsMouseButtonDown(MouseButton button);
        public bool IsMouseButtonUp(MouseButton button);
        public bool IsMouseButtonPressed(MouseButton button);
        public bool IsMouseButtonReleased(MouseButton button);
        public Vector2 MousePosition { get; }
        public void Update();
    }
}
