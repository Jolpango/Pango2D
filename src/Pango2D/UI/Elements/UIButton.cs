using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.Core.Input.Contracts;

namespace Pango2D.UI.Elements
{
    public class UIButton: UIElement
    {
        private Color currentColor;
        public Color HoverColor { get; set; }
        public Color PressColor { get; set; }
        public UIButton(GameWindow gameWindow, FontRegistry fontRegistry) : base(gameWindow, fontRegistry)
        {
            FontSize = 24; // Default font size
            FontColor = Color.Black;
            HoverColor = Color.DarkGray; // Default hover color
            BackgroundColor = Color.White; // Default background color
            PressColor = Color.Gray; // Default press color
            currentColor = BackgroundColor;
            Padding = new Point(20, 10); // Default padding
        }
        public override void Update(GameTime time, IInputProvider input)
        {
            base.Update(time, input);
            if (isMousePressing)
            {
                currentColor = PressColor;
            }
            else if (wasMouseOver)
            {
                currentColor = HoverColor;
            }
            else
            {
                currentColor = BackgroundColor;
            }
        }
        protected override void DrawBackgroundColor(SpriteBatch spriteBatch)
        {
            if (BackgroundColor != Color.Transparent)
            {
                spriteBatch.Draw(TextureCache.White, new Rectangle(Position, Size), currentColor);
            }
        }
    }
}
