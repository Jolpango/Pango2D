using Microsoft.Xna.Framework;
using Pango2D.Core.Graphics;

namespace Pango2D.UI.Elements
{
    public class UILabel : UIElement
    {
        public UILabel(GameWindow gameWindow, FontRegistry fontRegistry) : base(gameWindow, fontRegistry)
        {
            FontSize = 12; // Default font size
            BackgroundColor = Color.White;
            Padding = new Point(5, 5);
            FontColor = Color.Black;
        }
    }
}
