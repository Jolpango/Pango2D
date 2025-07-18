using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pango2D.Core.Input
{
    public class GamePadManager
    {
        public GamePadState[] previousGamePadStates = new GamePadState[4];
        public GamePadState[] currentGamePadStates = new GamePadState[4];
        public GamePadManager()
        {
            for (int i = 0; i < 4; i++)
            {
                previousGamePadStates[i] = GamePad.GetState((PlayerIndex)i);
                currentGamePadStates[i] = previousGamePadStates[i];
            }
        }
        public void Update()
        {
            for (int i = 0; i < 4; i++)
            {
                previousGamePadStates[i] = currentGamePadStates[i];
                currentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);
            }
        }
        public Vector2 GetLeftThumbstick(PlayerIndex playerIndex)
        {
            return currentGamePadStates[(int)playerIndex].ThumbSticks.Left * new Vector2(1, -1);
        }
        public Vector2 GetRightThumbstick(PlayerIndex playerIndex)
        {
            return currentGamePadStates[(int)playerIndex].ThumbSticks.Right * new Vector2(1, -1);
        }
        public bool IsButtonDown(PlayerIndex playerIndex, Buttons button)
        {
            return currentGamePadStates[(int)playerIndex].IsButtonDown(button);
        }
        public bool IsButtonPressed(PlayerIndex playerIndex, Buttons button)
        {
            return currentGamePadStates[(int)playerIndex].IsButtonDown(button) &&
                   previousGamePadStates[(int)playerIndex].IsButtonUp(button);
        }
        public bool IsButtonReleased(PlayerIndex playerIndex, Buttons button)
        {
            return currentGamePadStates[(int)playerIndex].IsButtonUp(button) &&
                   previousGamePadStates[(int)playerIndex].IsButtonDown(button);
        }
        public bool IsButtonUp(PlayerIndex playerIndex, Buttons button)
        {
            return currentGamePadStates[(int)playerIndex].IsButtonUp(button);
        }
    }
}
