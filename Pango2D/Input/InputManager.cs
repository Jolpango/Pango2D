using Microsoft.Xna.Framework.Input;
using Pango2D.Input.Contracts;
using System;
using System.Numerics;

namespace Pango2D.Input
{
    public class InputManager : IInputProvider
    {
        private MouseState previousMouseState;
        private MouseState currentMouseState;

        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;

        public InputManager()
        {
            previousMouseState = Mouse.GetState();
            previousKeyboardState = Keyboard.GetState();
            currentMouseState = previousMouseState;
            currentKeyboardState = previousKeyboardState;
        }
        public Vector2 MousePosition => throw new NotImplementedException();

        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            return GetButtonState(currentMouseState, button) == ButtonState.Pressed;
        }

        public bool IsMouseButtonPressed(MouseButton button)
        {
            return GetButtonState(currentMouseState, button) == ButtonState.Pressed &&
                   GetButtonState(previousMouseState, button) == ButtonState.Released;
        }

        public bool IsMouseButtonReleased(MouseButton button)
        {
            return GetButtonState(currentMouseState, button) == ButtonState.Released &&
                   GetButtonState(previousMouseState, button) == ButtonState.Pressed;
        }

        public bool IsMouseButtonUp(MouseButton button)
        {
            return GetButtonState(currentMouseState, button) == ButtonState.Released;
        }

        public void Update()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        private ButtonState GetButtonState(MouseState state, MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => state.LeftButton,
                MouseButton.Right => state.RightButton,
                MouseButton.Middle => state.MiddleButton,
                MouseButton.XButton1 => state.XButton1,
                MouseButton.XButton2 => state.XButton2,
                _ => ButtonState.Released,
            };
        }
    }
}
