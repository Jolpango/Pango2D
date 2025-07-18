using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pango2D.ECS.Components.Contracts;

namespace PlatformerDemo.Components
{
    public class FighterControls : IComponent
    {
        public Buttons JumpButton { get; set; } = Buttons.A;
        public Buttons AttackButton { get; set; } = Buttons.X;
        public Buttons SignatureButton { get; set; } = Buttons.Y;
        public Buttons DashLeftButton { get; set; } = Buttons.LeftShoulder;
        public Buttons DashRightButton { get; set; } = Buttons.RightShoulder;
        public PlayerIndex PlayerIndex { get; set; } = PlayerIndex.One;
    }
}
